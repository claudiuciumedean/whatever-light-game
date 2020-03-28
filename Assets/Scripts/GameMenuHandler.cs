using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuHandler : MonoBehaviour
{
    public GameObject pausePanel;

    public void pauseGame() {
        pausePanel.SetActive(true);
    }

    public void resumeGame() {
        pausePanel.SetActive(false);
    }

    public void restartLevel() {
        GameManager.Instance.restartLevel();
    }

    public void showStartScreen() {
        GameManager.Instance.showStartMenuPanel();
    }

    public void nextLevel() {
        GameManager.Instance.completeLevel();
    }
}
