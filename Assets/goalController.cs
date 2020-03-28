using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalController : MonoBehaviour
{
    public Sprite achievedGoal;
    public SpriteRenderer spriteRenderer;
    public GameObject levelCompletePanel;

    public void goalAchieved()
    {
        spriteRenderer.sprite = achievedGoal;
        levelCompletePanel.SetActive(true);
    }
}
