using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public float chaseSpeed, catchDistance, jumpscareTime;
    public Transform player;
    public Camera mainCamera; // Referensi ke kamera utama
    public Camera jumpscareCamera; // Referensi ke kamera jumpscare
    private Vector3 initialPlayerPosition; // Menyimpan posisi awal pemain
    public Vector3 rayCastOffset;

    void Start()
    {
        initialPlayerPosition = player.position; // Simpan posisi awal pemain
        if (jumpscareCamera != null)
        {
            jumpscareCamera.gameObject.SetActive(false); // Nonaktifkan kamera jumpscare pada awalnya
        }
    }

    void Update()
    {
        // Selalu set destination musuh ke posisi pemain
        ai.destination = player.position;
        ai.speed = chaseSpeed;

        // Memastikan NavMeshAgent mengikuti jalur
        if (ai.pathPending || ai.pathStatus == NavMeshPathStatus.PathPartial)
        {
            return; // Jika path belum siap atau tidak lengkap, keluar
        }

        float distance = Vector3.Distance(player.position, ai.transform.position);
        if (distance <= catchDistance)
        {
            Debug.Log("Player caught!");
            player.gameObject.SetActive(false);
            StartCoroutine(deathRoutine());
        }
    }

    IEnumerator deathRoutine()
    {
        if (jumpscareCamera != null && mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false); // Nonaktifkan kamera utama
            jumpscareCamera.gameObject.SetActive(true); // Aktifkan kamera jumpscare
        }
        yield return new WaitForSeconds(jumpscareTime);
        if (jumpscareCamera != null && mainCamera != null)
        {
            jumpscareCamera.gameObject.SetActive(false); // Nonaktifkan kamera jumpscare
            mainCamera.gameObject.SetActive(true); // Aktifkan kembali kamera utama
        }
        player.position = initialPlayerPosition; // Kembalikan posisi pemain ke posisi awal
        player.gameObject.SetActive(true); // Aktifkan kembali pemain
        // Kembalikan musuh ke posisi awal jika diperlukan
        ai.destination = transform.position; // Set destination ke posisi musuh untuk berhenti
        ai.speed = 0;
    }
}
