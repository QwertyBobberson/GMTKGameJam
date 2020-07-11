using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] spawnpoints;
    public float spawntime;
    public GameObject enemy;

    float timeSinceLastSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timeSinceLastSpawn);
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > spawntime)
        {
            Debug.Log("hm");

            timeSinceLastSpawn = 0.0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        GameObject.Instantiate(enemy, spawnpoints[0].transform.position, spawnpoints[0].transform.rotation);
    }
}
