using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }
}
