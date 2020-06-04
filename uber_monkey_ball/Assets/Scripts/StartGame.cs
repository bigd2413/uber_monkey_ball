using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;

    public bool gameStarted;
    public GameObject startText;
    // Start is called before the first frame update
    void Start()
    {
        startPanel = gameObject;

        startText.GetComponent<Text>().text = "Welcome to Uber Monkey Ball!\n\n" +
                                         "Your goal is to get to the end of each level as fast as can and\n" +
                                         "to collect as many bones as possible.\n\n" +
                                         "Controls:\n" +
                                         "Movement: Tilt the level with the WASD keys or control stick to roll\n" +
                                         "your monkey skull!\n\n" +
                                         "Pause: ESC key or Start Button\n\n" +
                                         "Press the space key or A Button to start.\n\n" +
                                         "The current high score to beat is " + PlayerPrefs.GetInt("HighScore", 0).ToString() +  ". Good luck!";

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Time.timeScale = 0;
            startPanel.GetComponent<CanvasGroup>().alpha = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && SceneManager.GetActiveScene().buildIndex == 0)
        {
            BeginGame();
        }
    }

    private void BeginGame()
    {
        Time.timeScale = 1;

        startPanel.GetComponent<CanvasGroup>().alpha = 0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
