using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Start() and Awake() don't run when a new scene is loaded for an object within DontDestroyOnLoad,
// so this was made separately to collect all scene references for GameStats when loading a new scene
public class SceneInitializer : MonoBehaviour
{
    public GameStats gameStats;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CollectSceneReferences());
    }

    IEnumerator CollectSceneReferences()
    {
        yield return new WaitForEndOfFrame();

        gameStats = FindObjectOfType<GameStats>();
        gameStats.InitializeGameStats();
    }


}
