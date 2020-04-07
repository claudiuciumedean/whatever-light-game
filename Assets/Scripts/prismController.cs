using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prismController : MonoBehaviour
{
    public GameObject redLight;
    public GameObject greenLight;
    public GameObject blueLight;

    public string direction;
    public string prevDirection;
    public string sourceDirection;
    public string prevSourceDirection;

    string redDir;
    string greenDir;
    string blueDir;

    Color redBeam = Color.red;
    Color greenBeam = Color.green;
    Color blueBeam = Color.blue;

    void Start()
    {
        prevDirection = "none";
        prevSourceDirection = "none";
    }

    void Update()
    {
        if (activationCheck())
        {
            redLight.GetComponent<prismBeamController>().updateBeam(redBeam, redDir);
            greenLight.GetComponent<prismBeamController>().updateBeam(greenBeam, greenDir);
            blueLight.GetComponent<prismBeamController>().updateBeam(blueBeam, blueDir);   
            redLight.GetComponent<prismBeamController>().active = true;
            greenLight.GetComponent<prismBeamController>().active = true;
            blueLight.GetComponent<prismBeamController>().active = true;
            redLight.GetComponent<LineRenderer>().enabled = true;
            greenLight.GetComponent<LineRenderer>().enabled = true;
            blueLight.GetComponent<LineRenderer>().enabled = true;
        } 
        else
        {
            redLight.GetComponent<prismBeamController>().active = false;
            greenLight.GetComponent<prismBeamController>().active = false;
            blueLight.GetComponent<prismBeamController>().active = false;
            redLight.GetComponent<LineRenderer>().enabled = false;
            greenLight.GetComponent<LineRenderer>().enabled = false;
            blueLight.GetComponent<LineRenderer>().enabled = false;
            //Invoke("delayTurnOffBeam", 0.1f);
        }
    }

    private void delayTurnOffBeam()
    {
        redLight.GetComponent<prismBeamController>().active = false;
        greenLight.GetComponent<prismBeamController>().active = false;
        blueLight.GetComponent<prismBeamController>().active = false;
        redLight.GetComponent<LineRenderer>().enabled = false;
        greenLight.GetComponent<LineRenderer>().enabled = false;
        blueLight.GetComponent<LineRenderer>().enabled = false;
    }


    bool activationCheck()
    {
        if (direction.Equals("up") && sourceDirection.Equals("down"))
        {
            redDir = "right";
            greenDir = "down";
            blueDir = "left";
            return true;
        }
        else if (direction.Equals("down") && sourceDirection.Equals("up"))
        {
            redDir = "right";
            greenDir = "up";
            blueDir = "left";
            return true;
        }
        else if (direction.Equals("right") && sourceDirection.Equals("left"))
        {
            redDir = "down";
            greenDir = "left";
            blueDir = "up";
            return true;
        }
        else if (direction.Equals("left") && sourceDirection.Equals("right"))
        {
            redDir = "up";
            greenDir = "right";
            blueDir = "down";
            return true;
        }
        else
            return false;
    }

}
