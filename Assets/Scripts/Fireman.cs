using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireman : MonoBehaviour
{
    public GameObject fire;
    public float maxRangeSquared;
    GameObject spawnedFire = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        GameObject closestEnemy = null;
        float distanceToClosestEnemy = float.MaxValue;

        Object[] enemies = GameObject.FindObjectsOfType<EnemyMovement>();
        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject currEnemy = ((EnemyMovement)enemies[i]).gameObject;
            if ((transform.position - currEnemy.transform.position).sqrMagnitude < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = (transform.position - currEnemy.transform.position).sqrMagnitude;
                closestEnemy = currEnemy;
            }
        }
        Debug.Log(distanceToClosestEnemy);
        if (distanceToClosestEnemy < maxRangeSquared)
        {
            if (spawnedFire == null)
            {
                spawnedFire = GameObject.Instantiate(fire, transform.position, Quaternion.FromToRotation(Vector2.up, transform.position - closestEnemy.transform.position));

            }
            else
            {
                spawnedFire.transform.rotation = Quaternion.FromToRotation(Vector2.up, transform.position - closestEnemy.transform.position);
            }
        } else if (spawnedFire != null)
        {
            Destroy(spawnedFire);
            spawnedFire = null;
        }
    }
}
