using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public AudioClip coinSound;
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.CompareTag("ball"))
        {
            Destroy(gameObject);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 1);
            FindObjectOfType<LevelManager>().PlaySound(coinSound);
            UiManager.instance.ChangeScore();
        }

    }

}
