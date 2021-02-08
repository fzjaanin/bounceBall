using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public Text scoreText;
    public Text levelText;
    public Text delayText;
    public GameObject GameOverPanel;
    public GameObject ResumePanel;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    
    public void ChangeLevel(int index)
    {
        levelText.text = "Level " + index;
        levelText.GetComponent<Animation>().Play();
    }

    
    public void GameOver()
    {
        ResumePanel.gameObject.SetActive(false);
        GameOverPanel.gameObject.SetActive(true);
    }

    
    public void ChangeScore()
    {
        scoreText.text = PlayerPrefs.GetInt("coins").ToString();
    }

    public void Resume()
    {
        ResumePanel.gameObject.SetActive(true);
        Timer.instance.startTimer(3);
    }

    public void DelayText(float delay)
    {
        delayText.text = delay.ToString("00");
    }

    public void HidePanels()
    {
        ResumePanel.gameObject.SetActive(false);
        GameOverPanel.gameObject.SetActive(false);
    }
   
}
