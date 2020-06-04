using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    public bool gamePaused;


    void Start()
    {
        gamePaused = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") && SceneManager.GetActiveScene().buildIndex > 0)
        {
            if (gamePaused == false)
            {
                PauseGame();
            }
            else
            {
                ContinueGame();
            }
        }
    }

    private void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;

        pausePanel.GetComponent<CanvasGroup>().alpha = 1f;
    }

    private void ContinueGame()
    {
        gamePaused = false;
        Time.timeScale = 1;

        pausePanel.GetComponent<CanvasGroup>().alpha = 0f;
    }
}
