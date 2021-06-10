using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private GameObject cheesePrefab;
    [SerializeField] private GameObject poisonPrefab;
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private float distanceThreshold = 100;
    private Vector3 nextPlatformPos = Vector3.zero;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(platformPrefab, nextPlatformPos, Quaternion.identity); // Gimbal Lock - Quaternions vs Euler Angles
        nextPlatformPos += new Vector3(0, 0, 55);
    }

    private void Update()
    {
        if (Vector3.Distance(nextPlatformPos, player.transform.position) < distanceThreshold)
        {
            GameObject plat = Instantiate(platformPrefab, nextPlatformPos, Quaternion.identity);
            // Add 3-5 obstacles within this plaform - randomly in both x & z direction
            for (int i = 0; i < 5; i++)
            {
                obstaclePrefab.transform.position = new Vector3(Random.Range(-2, 2), 1, Random.Range(-20, 20));
                GameObject obs = Instantiate(obstaclePrefab, nextPlatformPos + obstaclePrefab.transform.position, Quaternion.identity);
                obs.transform.parent = plat.transform;
            }
            for (int i = 0; i < 2; i++)
            {
                slimePrefab.transform.position = new Vector3(Random.Range(-2, 2), 1, Random.Range(-20, 20));
                GameObject sli = Instantiate(slimePrefab, nextPlatformPos + slimePrefab.transform.position, Quaternion.identity);
                sli.transform.parent = plat.transform;
            }
            for (int i = 0; i < 5; i++)
            {
                cheesePrefab.transform.position = new Vector3(Random.Range(-2, 2), 1, Random.Range(-20, 20));
                GameObject chs = Instantiate(cheesePrefab, nextPlatformPos + cheesePrefab.transform.position, Quaternion.identity);
                chs.transform.parent = plat.transform;
            }
            poisonPrefab.transform.position = new Vector3(Random.Range(-2, 2), 1, Random.Range(-20, 20));
            GameObject pois = Instantiate(poisonPrefab, nextPlatformPos + poisonPrefab.transform.position, Quaternion.identity);
            pois.transform.parent = plat.transform;

            healthPrefab.transform.position = new Vector3(Random.Range(-2, 2), 1, Random.Range(-20, 20));
            GameObject heal = Instantiate(healthPrefab, nextPlatformPos + healthPrefab.transform.position, Quaternion.identity); ;
            heal.transform.parent = plat.transform;

            nextPlatformPos += new Vector3(0, 0, 55);
        }
    }
}
