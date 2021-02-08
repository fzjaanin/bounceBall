using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    private float delay ;
    private bool started;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void startTimer(float delay)
    {
        this.delay = delay;
        started = true;
    }
    void Update()
    {
        if (started)
        {
            if (delay > 0)
            {
                delay -= Time.unscaledDeltaTime;
                UiManager.instance.DelayText(delay);
            }
            else
            {
                UiManager.instance.GameOver();
                started = false;
            }
        }
    }


}
