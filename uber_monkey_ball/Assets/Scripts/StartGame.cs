using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;

    public bool gameStarted;
    // Start is called before the first frame update
    void Start()
    {
        startPanel = gameObject;

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
