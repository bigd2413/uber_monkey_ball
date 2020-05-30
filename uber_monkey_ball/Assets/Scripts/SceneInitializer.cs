using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    public GameStats gameStats;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SceneInitializer script executing.");

        StartCoroutine(CollectSceneReferences());

    }

    IEnumerator CollectSceneReferences()
    {
        yield return new WaitForEndOfFrame();

        gameStats = FindObjectOfType<GameStats>();
        gameStats.InitializeGameStats();
    }


}
