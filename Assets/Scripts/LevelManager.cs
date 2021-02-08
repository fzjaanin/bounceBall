using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
 
    public GameObject coin;
    public GameObject[] level;
    public GameObject speedEffect;

    public int levelIndex;
    public bool started;
    public bool finished;

   
    public AudioClip destroySound;
    public AudioClip failedSound;

    private List<Color> colors = new List<Color>();
    private GameObject lastPlatform;
    private GameObject actualLevel;
    public GameObject fingerTuto;
    private GameObject finger;


    private void Start()
    {
        finger = Instantiate(fingerTuto);
        GetLevel();
       UiManager.instance.ChangeScore();

    }

    private void GetLevel()
    {
        levelIndex = PlayerPrefs.GetInt("level");
        actualLevel = Instantiate(level[levelIndex]);
        UiManager.instance.ChangeLevel(levelIndex + 1);
        actualLevel.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().material.color = FindObjectOfType<Ball>().GetComponent<MeshRenderer>().material.color;

        for (int i = 0; i < actualLevel.transform.childCount - 1; i++)
        {
            colors.Add(actualLevel.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color);
        }

    }

    public void DeleteColor(GameObject platform)
    {
        foreach (Color col in colors)
        {
            if (platform.gameObject.GetComponent<MeshRenderer>().material.color == col)
            {
                colors.Remove(col);
                PlaySound(destroySound);
                break;

            }
        }

        if (colors.Count == 0)
        {
            GameObject effect = Instantiate(speedEffect);
            Destroy(effect, 2);
            Destroy(actualLevel);
            levelIndex++;
            PlayerPrefs.SetInt("level", levelIndex);
            GetLevel();
            actualLevel.GetComponent<Animation>().Play();
            


        }
        else
        {
            FindObjectOfType<Ball>().ChangeColor(colors[Random.Range(0, colors.Count)]);

        }
    }

    public void MakeCoins(GameObject platform)
    {
        GameObject clone;

        if (lastPlatform != platform)
        {
            if (lastPlatform)
            {
                if (lastPlatform.transform.childCount == 0)
                {
                    clone = Instantiate(coin, lastPlatform.transform.position + Vector3.up, Quaternion.identity);
                    clone.transform.SetParent(lastPlatform.transform);
                }
            }


            lastPlatform = platform;
        }


    }



    public void DestroyFinger()
    {
        Destroy(finger);
    }

    public void PlaySound(AudioClip sound)
    {
        GetComponent<AudioSource>().PlayOneShot(sound);
    }


    public void GameOver()
    {
        PlaySound(failedSound);
        Time.timeScale = 0;
        UiManager.instance.Resume();
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void Resume()
    {
        UiManager.instance.HidePanels();
        finished = false;
        FindObjectOfType<Ball>().ResetPosition();
        finger = Instantiate(fingerTuto);
        StartCoroutine("Wait");
        
    }
 
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        DestroyFinger();
    }
    



}
