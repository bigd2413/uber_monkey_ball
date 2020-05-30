using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    public int deathCount;
    public GameObject player;
    public GameObject gameManager;
    public GameObject deathTextObject;

    // We want one instance of this to carry over to other scenes so music doesn't get interrupted.
    public static GameStats instance;

    public static GameStats GetInstance()
    {
        return instance;
    }


    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("Awake in GameStats is being executed.");
    }

    public void InitializeGameStats()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager");
        deathTextObject = GameObject.FindWithTag("DeathText");
        deathTextObject.GetComponent<Text>().text = "Deaths: " + deathCount.ToString(); 


        PlayerController pc = player.GetComponent<PlayerController>();
        pc.PlayerFalloutEvent += IncreaseDeathCount;

        Timer timerScript = gameManager.GetComponent<Timer>();
        timerScript.TimeoutEvent += IncreaseDeathCount;


        

        Debug.Log("InitializeGamestats is being executed.");
    }

    public void IncreaseDeathCount()
    {
        deathCount++;
        deathTextObject.GetComponent<Text>().text = "Deaths: " + deathCount.ToString(); 
    }
}
