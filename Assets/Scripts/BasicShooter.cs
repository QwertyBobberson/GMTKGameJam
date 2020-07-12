using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShooter : MonoBehaviour
{
    public float maxRangeSquared;
    public float damage;
    public float health;
    public float reloadTime;
    public GameObject bullet;
    public float bulletSpeed;

    AudioSource audio;
    float timeSinceShoot = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceShoot += Time.deltaTime;
        if (timeSinceShoot > reloadTime)
        {
            Shoot();
            timeSinceShoot = 0.0f;
        }
    }

    void Shoot()
    {
        audio.Play();
        GameObject closestEnemy = null;
        float distanceToClosestEnemy = float.MaxValue;

        Object[] enemies = GameObject.FindObjectsOfType<EnemyMovement>();
        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject currEnemy = ((EnemyMovement)enemies[i]).gameObject;
            if((transform.position - currEnemy.transform.position).sqrMagnitude < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = (transform.position - currEnemy.transform.position).sqrMagnitude;
                closestEnemy = currEnemy;
            }
        }
        if (distanceToClosestEnemy < maxRangeSquared)
        {
            GameObject spawnedBullet = GameObject.Instantiate(bullet, transform.position, Quaternion.FromToRotation(transform.position, closestEnemy.transform.position));
            BasicBullet bulletScript = spawnedBullet.GetComponent<BasicBullet>();
            bulletScript.damageAmt = 10;
            spawnedBullet.GetComponent<Rigidbody2D>().velocity = (closestEnemy.transform.position - transform.position).normalized * bulletSpeed;
        }
    }
}
