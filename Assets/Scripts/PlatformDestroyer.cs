using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    [SerializeField] private float distanceThreshold = 100;
    [SerializeField] private bool isStepped = false;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (isStepped && Vector3.Distance(gameObject.transform.position, player.transform.position) > distanceThreshold) // && player.transform.position.z > gameObject.transform.position.z
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
            isStepped = true;
    }
}
