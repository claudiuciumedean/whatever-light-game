using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightController : MonoBehaviour
{
    public string direction;
    public GameObject initialHit;
    public Color lightColor = Color.white;

    private LineRenderer lineRenderer;
    private Vector3 lightDirection;
    private string previousDirection;

    GameObject lastHit;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        previousDirection = "none";
        lineRenderer.startColor = lightColor;
        lineRenderer.endColor = lightColor;
    }

    void Update()
    {
        if (direction != previousDirection)
        {
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
            //lastHit = Physics2D.Raycast(transform.position, lightDirection).transform.gameObject;
            //lineRenderer.SetPosition(1, lastHit.transform.position);
            if (lastHit != null && lastHit.tag.Contains("Reflective"))
            {
                lastHit.GetComponent<mirrorController>().interaction = false;
            }
            lastHit = initialHit;
            previousDirection = direction;
        }
             
        RaycastHit2D hit = Physics2D.Raycast(transform.position + lightDirection/100, lightDirection);
        
        if (hit.transform.position != lastHit.transform.position)
        {
            lineRenderer.SetPosition(0, transform.position);
            if (hit.transform.tag.Contains("Reflective"))
            {
                lineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, transform.position.z));
                if (hit.transform.GetComponent<mirrorController>().numberOfInteractions < 2)
                    hit.transform.GetComponent<mirrorController>().numberOfInteractions++;
                Invoke("delayNextMirror", 0.1f);
                //if (hit.transform.GetComponent<mirrorController>().numberOfInteractions < 2)
                //{
                //    hit.transform.GetComponent<mirrorController>().sourceDirection = direction;
                //    hit.transform.GetComponent<mirrorController>().interaction = true;
                //}
            }
            else
            {
                lineRenderer.SetPosition(1, lightDirection * 2000);
            }
            if (lastHit.tag.Contains("Reflective"))
            {
                if (lastHit.GetComponent<mirrorController>().numberOfInteractions < 2)
                    lastHit.GetComponent<mirrorController>().interaction = false;
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

    public void updateLightColor(Color color)
    {
        lightColor = color;
        lineRenderer.startColor = lightColor;
        lineRenderer.endColor = lightColor;
    }
    private void delayNextMirror()
    {
        if (lastHit.tag.Contains("Reflective"))
        {
            if (lastHit.GetComponent<mirrorController>().numberOfInteractions < 2)
            {
                lastHit.GetComponent<mirrorController>().sourceDirection = direction;
                lastHit.GetComponent<mirrorController>().updateLightColor(lightColor);
                lastHit.GetComponent<mirrorController>().interaction = true;
            }
        }
    }


}
