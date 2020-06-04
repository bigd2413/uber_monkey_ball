using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BonusLevel : MonoBehaviour
{
    public GameStats gameStats;
    public int levelDeathCount;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        gameStats = FindObjectOfType<GameStats>();
        levelDeathCount = gameStats.levelDeaths;

        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1 && levelDeathCount == 0)
        {
            StartCoroutine(ShowBonusText());
        }



    }

    IEnumerator ShowBonusText()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1f;
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<CanvasGroup>().alpha = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
