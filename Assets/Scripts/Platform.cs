using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private int counter, rand;
    private LevelManager levelManager;

    public int min, max;
    public GameObject circleEffect;
    public GameObject destroyEffect;


    private void Start()
    {
         levelManager = FindObjectOfType<LevelManager>();
         rand = Random.Range(min, max);
    }
    private void Update()
    {
        if (transform.parent.gameObject.transform.localRotation.x!=0)
        {
            GetComponent<TrailRenderer>().emitting = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (levelManager.started==true)
        {
            

            if (GetComponent<MeshRenderer>().material.color != collision.gameObject.GetComponent<MeshRenderer>().material.color)
            {
                levelManager.finished = true;
                levelManager.GameOver();

            }

            else if (counter < rand)
            {
                counter++;
                GameObject clone = Instantiate(circleEffect);
                clone.transform.SetParent(this.gameObject.transform);
                clone.transform.localScale = new Vector3(1, 1, 1);
                clone.transform.localPosition = new Vector3(0, 0, 0);
                Destroy(clone, 1);
                levelManager.MakeCoins(this.gameObject);
                

            }

            else
            {
            
                Destroy(this.transform.parent.gameObject);
                GameObject clone = Instantiate(destroyEffect);
                Destroy(clone, 1);
                var main = clone.GetComponent<ParticleSystem>().main;
                main.startColor = this.gameObject.GetComponent<MeshRenderer>().material.color;
                levelManager.DeleteColor(this.gameObject);
                

            }

        }
    }

}
