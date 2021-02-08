using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("ball"))
        {
            FindObjectOfType<LevelManager>().GameOver();
            FindObjectOfType<LevelManager>().finished = true;
        }

    }
}
