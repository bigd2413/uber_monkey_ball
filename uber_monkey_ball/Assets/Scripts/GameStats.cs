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
    public int boneScore;
    public int timeScore;
    public int deathScore;
    public int levelScore;
    public int score;
    public GameObject player;
    public GameObject gameManager;
    public GameObject deathTextObject;
    public GameObject boneTextObject;
    public GameObject scoreTextObject;
    public GameObject levelScoreSummaryTextObject;
    public GameObject levelTotalScoreTextObject;
    public GameObject endLevelPanel;
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
        endLevelPanel = GameObject.FindWithTag("EndLevelPanel");

        endLevelPanel.GetComponent<CanvasGroup>().alpha = 0f;



        PlayerController pc = player.GetComponent<PlayerController>();
        pc.PlayerFalloutEvent += IncreaseDeathCount;
        pc.GoalEvent += CalculateLevelScore;

        timerScript = gameManager.GetComponent<Timer>();
        timerScript.TimeoutEvent += IncreaseDeathCount;
    }

    public void IncreaseDeathCount()
    {
        deathCount++;
        levelDeaths++;
        deathTextObject.GetComponent<Text>().text = "Deaths: " + deathCount.ToString();

        boneCount = boneCount - levelBones;
        levelBones = 0;
    }

    public void IncreaseBoneCount(int numBones)
    {
        boneCount = boneCount + numBones;
        levelBones = levelBones + numBones;
        boneTextObject.GetComponent<Text>().text = "Bones: " + boneCount.ToString(); 
    }

    public void CalculateLevelScore()
    {
        StartCoroutine(CalcScoreRoutine());
    }

    IEnumerator CalcScoreRoutine()
    {
        yield return new WaitForSeconds(2.5f);

        levelTime = (Mathf.Round(timerScript.time * 10))/10;

        boneScore = levelBones * 100;
        timeScore = (int) (levelTime * 10);
        deathScore = levelDeaths * -100;
        levelScore = boneScore + timeScore + deathScore;
        score = score + levelScore;

        levelScoreSummaryTextObject = GameObject.FindWithTag("LevelTotalScoreSummaryText");
        levelScoreSummaryTextObject.GetComponent<Text>().text = "Bones Collected: " + levelBones.ToString("0") + " = "
                                                                + boneScore.ToString("00000") + " points\n" +
                                                                "Time Left: " + levelTime.ToString("0.0") + " = "
                                                                + timeScore.ToString("00000") + " points\n" +
                                                                "Deaths: " + levelDeaths.ToString("0") + " = "
                                                                + deathScore.ToString("00000") + " points\n" +
                                                                "Level Score: " + levelScore.ToString("00000") + " points";

        levelTotalScoreTextObject = GameObject.FindWithTag("LevelTotalScoreText");
        levelTotalScoreTextObject.GetComponent<Text>().text = "Total Score: " + score.ToString("00000");

        endLevelPanel.GetComponent<CanvasGroup>().alpha = 1f;

        levelDeaths = 0;
        levelBones = 0;
    }
}
