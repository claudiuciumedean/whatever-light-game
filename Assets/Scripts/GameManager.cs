using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    List<int> levelsIdx = new List<int> { 0, 1, 2, 3, 4, 5 };
    int currentLevel;

    public void startGame() {
        SceneManager.LoadScene("Main");
    }

    public void pauseGame() {
        Debug.Log("pausing the game");
    }

    public void goToNextLevel() {
        Debug.Log("Go to next level");
    }
}
