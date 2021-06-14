using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Vector3 spawnPos = new Vector3(20,0,0);

    public float startDelay = 2;
    public float repeatRate = 2;

    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(SpawnObstacle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnObstacle()
    {
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            if (!playerController.gameOver)
            {
                int rnd = Random.Range(0, obstaclePrefabs.Length);
                Instantiate(obstaclePrefabs[rnd], spawnPos, obstaclePrefabs[rnd].transform.rotation);
                yield return new WaitForSeconds(repeatRate);
            }
            else
            {
                yield return null;
            }
        }
    }
}
