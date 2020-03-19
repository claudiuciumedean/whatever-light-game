using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightController : MonoBehaviour
{
    public string direction;
    public GameObject initialHit;

    private LineRenderer lineRenderer;
    private Vector3 lightDirection;
    private string previousDirection;

    GameObject lastHit;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        previousDirection = "none";
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
            if (lastHit != null && lastHit.tag == "reflective")
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
            if (hit.transform.tag == "reflective")
            {
                lineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, transform.position.z));
                if (hit.transform.GetComponent<mirrorController>().numberOfInteractions < 2)
                    hit.transform.GetComponent<mirrorController>().numberOfInteractions++;
                if (hit.transform.GetComponent<mirrorController>().numberOfInteractions < 2)
                {
                    hit.transform.GetComponent<mirrorController>().interaction = true;
                    hit.transform.GetComponent<mirrorController>().sourceDirection = direction;
                }
            }
            else
            {
                lineRenderer.SetPosition(1, lightDirection * 2000);
            }
            if (lastHit.tag == "reflective")
            {
                if (lastHit.GetComponent<mirrorController>().numberOfInteractions < 2)
                    lastHit.GetComponent<mirrorController>().interaction = false;
                if (lastHit.GetComponent<mirrorController>().numberOfInteractions > 0)
                    lastHit.GetComponent<mirrorController>().numberOfInteractions--;
            }
            lastHit = hit.transform.gameObject;
        }
        if (true) //TODO performance improvement
        {
            lineRenderer.SetPosition(0, transform.position);
            if (hit.transform.tag == "reflective")
            {
                lineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, transform.position.z));
            }
            else
            {
                lineRenderer.SetPosition(1, lightDirection * 2000);
            }
        }
    }
}
