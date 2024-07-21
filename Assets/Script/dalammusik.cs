using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class dalammusik : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public GameObject light; // Referensi ke GameObject lampu
    public GameObject textObject; // Referensi ke GameObject teks

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StopMediaAfterDelay(5f));
        }
    }

    IEnumerator StopMediaAfterDelay(float delay)
    {
        // Hentikan audio dan video setelah delay
        yield return new WaitForSeconds(delay);

        if (audioSource != null)
        {
            audioSource.Stop();
        }
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }

        // Matikan lampu
        if (light != null)
        {
            light.SetActive(false);
        }

        // Tampilkan teks
        if (textObject != null)
        {
            textObject.SetActive(true);
        }

        // Tunggu selama 5 detik sebelum menghilangkan teks
        yield return new WaitForSeconds(5f);

        // Matikan teks
        if (textObject != null)
        {
            textObject.SetActive(false);
        }

        // Tunggu selama 1 detik
        yield return new WaitForSeconds(1f);

        // Nyalakan kembali lampu
        if (light != null)
        {
            light.SetActive(true);
        }
    }
}
