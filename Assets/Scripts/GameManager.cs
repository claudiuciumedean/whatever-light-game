using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    List<int> levelsIdx = new List<int> { 2, 3, 4, 5 };
    int currentLevel;

    public void showStartMenuPanel() {
        SceneManager.LoadScene("StartMenuScene");
    }

    public void showSelectLevelPanel() {
        SceneManager.LoadScene("LevelSelectScene");
    }

    public void startGame() {
        currentLevel = 0;
        SceneManager.LoadScene(levelsIdx[currentLevel]);
    }

    public void completeLevel() {
        this.loadLevel(++currentLevel);
    } 

    public void loadLevel(int levelIndex) {
        if(levelIndex == levelsIdx.Count) {
            this.showStartMenuPanel();
            return;
        }

        currentLevel = levelIndex;
        SceneManager.LoadScene(levelsIdx[currentLevel]);       
    }
}
