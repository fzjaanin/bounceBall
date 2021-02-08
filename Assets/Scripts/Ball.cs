using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float force;
    public AudioClip bounceSound;


    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
   }


    public void ChangeColor(Color col)
    {
        GetComponent<MeshRenderer>().material.color = col; 
    }
 

    private void OnCollisionEnter(Collision col)
    {
        if (!FindObjectOfType<LevelManager>().finished)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            FindObjectOfType<LevelManager>().PlaySound(bounceSound);
        }
    }

    public void ResetPosition()
    {
        this.transform.position = new Vector3(0, 36f, -4.92f);
        rb.velocity = Vector3.zero;
    }

}                                                                                                                                                                                                                                       
