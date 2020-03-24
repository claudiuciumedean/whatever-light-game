using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame() {
        GameManager.Instance.startGame();
    }

    public void pauseGame() {
        GameManager.Instance.pauseGame();
    }

    public void goToNextLevel() {
        GameManager.Instance.goToNextLevel();
    }
}
