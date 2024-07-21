using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareLight : MonoBehaviour
{
    public AudioSource scareSound;
    public Collider collision;
    public GameObject Jumpscare;
    public GameObject light; // Lampu yang akan dimatikan
    public Renderer lightBulb;
    public Material offlight;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Jumpscare.SetActive(true);
            collision.enabled = false;
            TurnOffLight();
            PlayScareSound();
        }
    }

    void TurnOffLight()
    {
        if (light != null)
        {
            light.SetActive(false);
            if (lightBulb != null && offlight != null)
            {
                lightBulb.material = offlight;
            }
        }
    }

    void PlayScareSound()
    {
        if (scareSound != null)
        {
            scareSound.Play();
        }
    }
}
