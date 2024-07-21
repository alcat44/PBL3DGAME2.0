using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pianika : MonoBehaviour
{
    public AudioSource doSound;
    public AudioSource reSound;
    public AudioSource miSound;
    public AudioSource faSound;
    public AudioSource soSound;
    public AudioSource laSound;
    public AudioSource siSound;
    public AudioSource highDoSound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            doSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            reSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            miSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            faSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            soSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            laSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            siSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            highDoSound.Play();
        }
    }
}
