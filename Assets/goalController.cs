using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalController : MonoBehaviour
{
    public Sprite achievedGoal;
    public SpriteRenderer spriteRenderer;

    public void goalAchieved()
    {
        spriteRenderer.sprite = achievedGoal;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
