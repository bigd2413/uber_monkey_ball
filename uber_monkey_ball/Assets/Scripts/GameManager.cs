using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static GameManager instance = null;
    public Transform player;
    public Camera mainCamera;
    public bool levelWarpAllowed;

    public Transform gameManager;

    // Game Instance Singleton
    public static GameManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        // if the singleton is not yet instantiated
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        levelWarpAllowed = false;        
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
        if (levelWarpAllowed == true && Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
