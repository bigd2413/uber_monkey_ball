using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BonusLevel : MonoBehaviour
{
    public GameStats gameStats;
    public int levelDeathCount;
    public int sceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(MaybeShowBonusText());

    }
    
    IEnumerator MaybeShowBonusText()
    {
        yield return new WaitForEndOfFrame();
        gameStats = FindObjectOfType<GameStats>();
        levelDeathCount = gameStats.levelDeaths;

        if (sceneIndex == SceneManager.sceneCountInBuildSettings - 1 && levelDeathCount == 0)
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
}
