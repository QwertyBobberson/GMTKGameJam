using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] spawnpoints;
    public float spawntime;
    public GameObject enemy;

    float timeSinceLastSpawn = 0.0f;

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > spawntime)
        {
            timeSinceLastSpawn = 0.0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        GameObject.Instantiate(enemy, spawnpoints[0].transform.position, spawnpoints[0].transform.rotation);
    }
}
