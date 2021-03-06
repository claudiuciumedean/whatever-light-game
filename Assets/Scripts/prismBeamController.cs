﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prismBeamController : MonoBehaviour
{
    public GameObject initialHit;
    public GameObject lastHit;
    public Color lightColor;
    public string direction;
    public bool active;

    private LineRenderer lineRenderer;
    private Vector3 lightDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lightDirection = Vector3.up;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + lightDirection / 100, lightDirection);

            if (hit.transform.position != lastHit.transform.position)
            {
                lineRenderer.SetPosition(0, transform.position);
                if (hit.transform.tag.Contains("Reflective"))
                {
                    if (hit.transform.GetComponent<mirrorController>().numberOfInteractions < 2 && !hit.transform.GetComponent<mirrorController>().reflection)
                    {
                        hit.transform.GetComponent<mirrorController>().sourceDirection = direction;
                        hit.transform.GetComponent<mirrorController>().updateLightColor(lightColor);
                        hit.transform.GetComponent<mirrorController>().interaction = true;
                    }
                    if (hit.transform.GetComponent<mirrorController>().numberOfInteractions < 2)
                        hit.transform.GetComponent<mirrorController>().numberOfInteractions++;
                    //Invoke("delayNextMirror", 0.1f);
                }
                else if (hit.transform.tag.Contains("Prism"))
                {
                    hit.transform.GetComponent<prismController>().changeSourceDir(direction, colorToString());
                    //hit.transform.GetComponent<prismController>().sourceColor = colorToString();
                }
                else if (hit.transform.tag.Contains("Crystal"))
                {
                    if (hit.transform.GetComponent<crystalController>().lightColor == lightColor || lightColor == Color.white)
                    {
                        hit.transform.GetComponent<crystalController>().updateBeam(direction);
                        hit.transform.GetComponent<crystalController>().active = true;
                    }
                    //hit.transform.GetComponent<prismController>().sourceColor = colorToString();
                }
                else if (hit.transform.tag.Contains("Goal"))
                {
                    hit.transform.GetComponent<goalController>().sourceColor = colorToString();
                    Invoke("delayAchievedGoal", 0.2f);
                }


                if (lastHit.tag.Contains("Prism"))
                {
                    if (lastHit.GetComponent<prismController>().sourceDirection == direction)
                        lastHit.GetComponent<prismController>().sourceDirection = "none";
                }
                if (lastHit.tag.Contains("Crystal"))
                {
                    lastHit.GetComponent<crystalController>().active = false;
                }
                if (lastHit.tag.Contains("Reflective"))
                {
                    if (lastHit.GetComponent<mirrorController>().numberOfInteractions < 2)
                    {
                        lastHit.GetComponent<mirrorController>().interaction = false;
                        lastHit.GetComponent<mirrorController>().sourceDirection = "none";

                    }
                    if (lastHit.GetComponent<mirrorController>().numberOfInteractions > 0)
                    {
                        lastHit.GetComponent<mirrorController>().numberOfInteractions--;
                    }
                }
                lastHit = hit.transform.gameObject;
            }
            if (true) //TODO performance improvement
            {
                lineRenderer.SetPosition(0, transform.position);
                if (hit)
                    lineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, transform.position.z));
                else
                    lineRenderer.SetPosition(1, lightDirection * 2000);
            }
        }
        else
        {   //reset
            if (lastHit.tag.Contains("Reflective"))
            {
                if (lastHit.GetComponent<mirrorController>().numberOfInteractions < 2)
                {
                    lastHit.GetComponent<mirrorController>().interaction = false;
                    lastHit.GetComponent<mirrorController>().sourceDirection = "none";

                }
                if (lastHit.GetComponent<mirrorController>().numberOfInteractions > 0)
                {
                    lastHit.GetComponent<mirrorController>().numberOfInteractions--;
                }

            }
            if (lastHit.tag.Contains("Prism"))
            {
                if (lastHit.GetComponent<prismController>().sourceDirection == direction)
                    lastHit.GetComponent<prismController>().sourceDirection = "none";
            }
            if (lastHit.tag.Contains("Crystal"))
            {
                lastHit.GetComponent<crystalController>().active = false;
            }
            lastHit = initialHit;
        }

    }

    public void updateBeam(Color color, string dir)
    {
        direction = dir;
        lightColor = color;
        lineRenderer.startColor = lightColor;
        lineRenderer.endColor = lightColor;

        switch (direction)
        {
            case "up":
                lightDirection = new Vector3(0, 1, 0);
                break;
            case "down":
                lightDirection = new Vector3(0, -1, 0);
                break;
            case "right":
                lightDirection = new Vector3(1, 0, 0);
                break;
            case "left":
                lightDirection = new Vector3(-1, 0, 0);
                break;
        }
    }

    private string colorToString()
    {
        string tmpColor = "white";
        if (lightColor == Color.red)
        {
            tmpColor = "red";
        }
        else if (lightColor == Color.green)
        {
            tmpColor = "green";
        }
        else if (lightColor == Color.blue)
        {
            tmpColor = "blue";
        }
        return tmpColor;
    }

    private void delayAchievedGoal()
    {
        if (lastHit.tag.Contains("Goal"))
            lastHit.GetComponent<goalController>().goalAchieved();
    }
    private void delayNextMirror()
    {
        if (lastHit.tag.Contains("Reflective"))
        {
            if (lastHit.GetComponent<mirrorController>().numberOfInteractions < 2 && !lastHit.GetComponent<mirrorController>().reflection)
            {
                lastHit.GetComponent<mirrorController>().sourceDirection = direction;
                lastHit.GetComponent<mirrorController>().updateLightColor(lightColor);
                lastHit.GetComponent<mirrorController>().interaction = true;
            }
            if (lastHit.GetComponent<mirrorController>().numberOfInteractions < 2)
                lastHit.GetComponent<mirrorController>().numberOfInteractions++;
        }
    }

}
