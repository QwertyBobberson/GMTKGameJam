using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] spawnpoints;
    public Wave[] wave;
    public float spawnTime;
    public float waveDelay;

    float currentTimer;
    float timeSinceLastSpawn = 0.0f;

    int x, y;

    private void Start()
    {
        x = y = 0;
        currentTimer = spawnTime;
    }

    void Update()
    {
        if(y == wave.Length)
        {
            return;
        }

        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > currentTimer)
        {
            timeSinceLastSpawn = 0.0f;
            SpawnEnemy(wave[y].array[x]);
            x++;

            if(x == wave[y].array.Length)
            {
                x = 0;
                y++;
                currentTimer = waveDelay;
            }
            else
            {
                currentTimer = spawnTime;
            }
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        GameObject.Instantiate(enemy, spawnpoints[0].transform.position, spawnpoints[0].transform.rotation);
    }
}

[System.Serializable]
public class Wave
{
    public GameObject[] array;
}
