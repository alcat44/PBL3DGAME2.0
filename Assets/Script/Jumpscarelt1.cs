using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscarelt1 : MonoBehaviour
{
    public GameObject Jumpscare;
    public AudioSource scareSound;
    public Collider collision;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Jumpscare.SetActive(true);
            //scareSound.Play();
            collision.enabled = false;
        }
    }
}