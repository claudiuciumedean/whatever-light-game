using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalController : MonoBehaviour
{
    public Sprite achievedGoal;
    public string sourceColor;
    public string pyramidColor;
    public SpriteRenderer spriteRenderer;
    public GameObject levelCompletePanel;

    public void goalAchieved()
    {
        if (pyramidColor == sourceColor)
        {
            spriteRenderer.sprite = achievedGoal;
            levelCompletePanel.SetActive(true);
        }  
    }
}
