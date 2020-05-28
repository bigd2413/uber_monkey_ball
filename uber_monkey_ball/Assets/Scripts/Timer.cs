using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public delegate void TimeoutEventHandler();
public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private float time;
    private bool timeOut;
    public event TimeoutEventHandler TimeoutEvent;

    // Start is called before the first frame update
    void Start()
    {
        startTime = 60f;
        time = startTime;
        timeOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        time = Mathf.Max(time, 0f);

        timerText.text = time.ToString("f1");

        if (time <= 0f & timeOut == false)
        {
            TimeoutEvent?.Invoke();
            timeOut = true;
            //GameManager.Instance.ManageTimeOut();
        }
    }
}
