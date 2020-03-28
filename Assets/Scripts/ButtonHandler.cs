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
    public void showStartMenuPanel() {
        GameManager.Instance.showStartMenuPanel();
    }
    public void showSelectLevelPanel() {
        GameManager.Instance.showSelectLevelPanel();
    }
    public void startGame() {
        GameManager.Instance.startGame();
    }
    public void loadLevel(int levelIndex) {
        GameManager.Instance.loadLevel(levelIndex);
    }
}
