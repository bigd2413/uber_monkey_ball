using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    public int deathCount;
    public int boneCount;
    public int levelDeaths;
    public int levelBones;
    public float levelTime;
    public int levelScore;
    public int score;
    public GameObject player;
    public GameObject gameManager;
    public GameObject deathTextObject;
    public GameObject boneTextObject;
    public GameObject scoreTextObject;
    public Timer timerScript;

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
    }

    public void InitializeGameStats()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager");
        deathTextObject = GameObject.FindWithTag("DeathText");
        deathTextObject.GetComponent<Text>().text = "Deaths: " + deathCount.ToString();
        boneTextObject = GameObject.FindWithTag("BoneText");
        boneTextObject.GetComponent<Text>().text = "Bones: " + boneCount.ToString();
        scoreTextObject = GameObject.FindWithTag("ScoreText");
        scoreTextObject.GetComponent<Text>().text = "Score: " + score.ToString("00000");


        PlayerController pc = player.GetComponent<PlayerController>();
        pc.PlayerFalloutEvent += IncreaseDeathCount;
        pc.GoalEvent += CalculateLevelScore;

        timerScript = gameManager.GetComponent<Timer>();
        timerScript.TimeoutEvent += IncreaseDeathCount;

        levelDeaths = 0;
        levelBones = 0;
        //levelScore = 0;
    }

    public void IncreaseDeathCount()
    {
        deathCount++;
        levelDeaths++;
        deathTextObject.GetComponent<Text>().text = "Deaths: " + deathCount.ToString();

        boneCount = boneCount - levelBones;
        levelBones = 0;
    }

    public void IncreaseBoneCount()
    {
        boneCount++;
        levelBones++;
        boneTextObject.GetComponent<Text>().text = "Bones: " + boneCount.ToString(); 
    }

    public void CalculateLevelScore()
    {
        levelTime = timerScript.time;
        levelScore = (levelBones * 100) + (int)(levelTime * 10) - (levelDeaths * 100);
        score = score + levelScore;
    }
}
