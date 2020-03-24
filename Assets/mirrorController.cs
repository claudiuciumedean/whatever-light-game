using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorController : MonoBehaviour
{
    public int corner;
    public GameObject initialHit;

    //[HideInInspector]
    public string sourceDirection;
    //[HideInInspector]
    public bool interaction;
    //[HideInInspector]
    public GameObject lastHit;

    public int numberOfInteractions;
    public bool reflection;
    private LineRenderer lineRenderer;
    private int previousCorner;
    private string previousSourceDirection;
    private string direction;
    private Vector3 verticalDirection;
    private Vector3 horizontalDirection;
    private Vector3 reflectionDirection;
    

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        interaction = false;
        reflection = false;
        previousCorner = 0;
        numberOfInteractions = 0;
        sourceDirection = "none";
        previousSourceDirection = "none";
        direction = "none";
    }

    // Update is called once per frame
    void Update()
    {
        if ((corner != previousCorner)||(sourceDirection != previousSourceDirection))
        {

            reflection = sourceCornerCheck();

            //lastHit = Physics2D.Raycast(transform.position, reflectionDirection).transform.gameObject;
            if (lastHit != null && lastHit.tag.Contains("Reflective"))
            {
                lastHit.GetComponent<mirrorController>().interaction = false;
            }
            lastHit = initialHit;
            previousSourceDirection = sourceDirection;
            previousCorner = corner;
        }

        if (interaction && reflection)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + reflectionDirection/100, reflectionDirection);
            if (!lineRenderer.enabled)
                lineRenderer.enabled = true;
            if (hit.transform.position != lastHit.transform.position)
            {
                lineRenderer.SetPosition(0, transform.position);
                if (hit.transform.tag.Contains("Reflective"))
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
                    lineRenderer.SetPosition(1, reflectionDirection * 2000);
                }
                if (lastHit.tag.Contains("Reflective"))
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
                if (hit.transform.tag.Contains("Reflective"))
                {
                    lineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, transform.position.z));
                }
                else
                {
                    lineRenderer.SetPosition(1, reflectionDirection * 2000);
                }
            }
        }
        else
        {   //reset
            if (lastHit.tag.Contains("Reflective"))
            {
                if (lastHit.GetComponent<mirrorController>().numberOfInteractions < 2)
                    lastHit.GetComponent<mirrorController>().interaction = false;
                if (lastHit.GetComponent<mirrorController>().numberOfInteractions > 0)
                    lastHit.GetComponent<mirrorController>().numberOfInteractions--;
            }
            lastHit = initialHit;
            //sourceDirection = "none";
            if (lineRenderer.enabled)
                lineRenderer.enabled = false;
        }


    }

    private bool sourceCornerCheck()
    {
        switch (sourceDirection)
        {
            case "up":
                if (corner == 1)        //top left corner
                {
                    reflectionDirection = new Vector3(1, 0, 0);     //right reflection
                    direction = "right";
                    return true;
                }
                    
                else if (corner == 2)       //top right corner
                {
                    reflectionDirection = new Vector3(-1, 0, 0);     //left reflection
                    direction = "left";
                    return true;
                }
                break;
            case "down":
                if (corner == 3)        //bottom right corner
                {
                    reflectionDirection = new Vector3(-1, 0, 0);     //left reflection
                    direction = "left";
                    return true;
                }
                    
                else if (corner == 4)       //bottom left corner
                {
                    reflectionDirection = new Vector3(1, 0, 0);     //right reflection
                    direction = "right";
                    return true;
                }
                break;
            case "right":
                if (corner == 2)        //top right corner
                {
                    reflectionDirection = new Vector3(0, -1, 0);     //down reflection
                    direction = "down";
                    return true;
                }
                    
                else if (corner == 3)       //bottom right corner
                {
                    reflectionDirection = new Vector3(0, 1, 0);     //up reflection
                    direction = "up";
                    return true;
                }
                break;
            case "left":
                if (corner == 1)        //top left corner
                {
                    reflectionDirection = new Vector3(0, -1, 0);     //down reflection
                    direction = "down";
                    return true;
                }
                    
                else if (corner == 4)       //bottom left corner
                {
                    reflectionDirection = new Vector3(0, 1, 0);     //up reflection
                    direction = "up";
                    return true;
                }
                break;   
        }
        return false;
    }
}
