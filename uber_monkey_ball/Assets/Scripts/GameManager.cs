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
        
    }
    // Update is called once per frame
    void Update()
    {
        
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
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator FalloffRoutine()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        FindObjectOfType<AudioManager>().Play("PlummetSound");

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
