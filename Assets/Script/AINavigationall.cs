using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AINavigationall : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime, jumpscareTime;
    public bool walking, chasing;
    public Transform player;
    public Camera mainCamera; // Referensi ke kamera utama
    public Camera jumpscareCamera; // Referensi ke kamera jumpscare
    private Vector3 initialPlayerPosition; // Menyimpan posisi awal pemain
    Transform currentDest;
    Vector3 dest;
    int randNum;
    public string deathScene;
    public Vector3 rayCastOffset;

    void Start()
    {
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
        initialPlayerPosition = player.position; // Simpan posisi awal pemain
        if (jumpscareCamera != null)
        {
            jumpscareCamera.gameObject.SetActive(false); // Nonaktifkan kamera jumpscare pada awalnya
        }
    }

    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                walking = false;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");
                chasing = true;
            }
        }

        if (chasing)
        {
            dest = player.position;
            ai.destination = dest;
            ai.speed = chaseSpeed;
            float distance = Vector3.Distance(player.position, ai.transform.position);
            if (distance <= catchDistance)
            {
                player.gameObject.SetActive(false);
                StartCoroutine(deathRoutine());
                chasing = false;
            }
        }
        else if (walking)
        {
            dest = currentDest.position;
            ai.destination = dest;
            ai.speed = walkSpeed;
            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                ai.speed = 0;
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                walking = false;
            }
        }
    }

    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }

    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        walking = true;
        chasing = false;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
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
        // Kembalikan musuh ke mode patroli
        walking = true;
        chasing = false; // Setel chasing ke false untuk memastikan musuh kembali ke mode patroli
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
        ai.destination = currentDest.position;
        ai.speed = walkSpeed;
    }
}