using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public Transform player;
    public Camera mainCamera;
    public bool levelWarpAllowed;
    public bool endScreenActive;

    public Transform gameManager;

    public GameStats gameStats;

    public int sceneCount;

    // Game Instance Singleton
    public static GameManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;

        // if the singleton is not yet instantiated
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        levelWarpAllowed = false;
        endScreenActive = false;
    }

    private void Start()
    {
        Timer timerScript = gameManager.GetComponent<Timer>();
        timerScript.TimeoutEvent += LevelRestart;

        PlayerController pc = player.GetComponent<PlayerController>();
        pc.GoalEvent += ManageGoal;
    }

    private void Update()
    {
        // Warp to next level on input
        if (levelWarpAllowed == true && Input.GetButtonDown("Fire1"))
        {
            if (SceneManager.GetActiveScene().buildIndex < sceneCount - 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if (!endScreenActive)// If last level, just show high score instead
            {
                gameStats = FindObjectOfType<GameStats>();
                gameStats.ShowHighScore();
                endScreenActive = true;
            }
            else // If endscreen panel is showing, user input to restart game
            {
                gameStats = FindObjectOfType<GameStats>();
                gameStats.RestartGame();
                SceneManager.LoadScene(0);
            }
        }
    }

    public void LevelRestart()
    {
        StartCoroutine(LevelRestartRoutine());
        return;
    }

    public void ManageGoal()
    {
        StartCoroutine(GoalRoutine());
    }

    public void ManageFalloff()
    {
        StartCoroutine(FalloffRoutine());
    }

    IEnumerator GoalRoutine()
    {
        FindObjectOfType<AudioManager>().Play("GoalSound");

        yield return new WaitForSeconds(2.5f);

        // Bool added so that the next level doesn't get warped to after a set amount of time,
        // but so that you can trigger next scene on an input detected in Update()
        levelWarpAllowed = true;
    }

    IEnumerator FalloffRoutine()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        FindObjectOfType<AudioManager>().Play("PlummetSound");

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LevelRestartRoutine()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<AudioManager>().Play("TimeOut");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
