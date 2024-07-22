using System.Collections;
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
            StartCoroutine(DisableJumpscareAfterDelay(15f)); // Memanggil Coroutine
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

    IEnumerator DisableJumpscareAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Jumpscare.SetActive(false); // Menghilangkan jumpscare setelah delay
    }
}
