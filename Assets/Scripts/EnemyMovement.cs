using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyMovement : MonoBehaviour
{
    GameObject[] pathNodes;
    int nextIndex = 0;
    Vector3 nextDestination;
    public float movementSpeed;
    public float maxHealth;
    public float health;
    public float damageAmt;
    public float reloadTime;
    public float lastPunchTime;
    bool dontMove = false;

    public float timeOfLastDamageFromBlocker = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        pathNodes = GameObject.FindObjectOfType<MapPath>().pathNodes;
        timeOfLastDamageFromBlocker = 0.0f;
        lastPunchTime = 0.0f;
        nextDestination = pathNodes[0].transform.position;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        dontMove = false;
        Object[] pathBlockers = GameObject.FindObjectsOfType<PathBlocker>();
        for (int i = 0; i < pathBlockers.Length; i++)
        {
            if (gameObject.GetComponent<CircleCollider2D>().IsTouching(((PathBlocker)pathBlockers[i]).gameObject.GetComponent<BoxCollider2D>()))
            {
                dontMove = true;
            }
        }
        if (!dontMove)
        {
            if ((nextDestination - transform.position).sqrMagnitude < 0.0001)
            {
                if (nextIndex < pathNodes.Length - 1)
                {
                    nextIndex++;
                    nextDestination = pathNodes[nextIndex].transform.position;
                }
                else
                {
                    PlayerStats.health--;
                    Die();
                }
            }
            else
            {
                transform.Translate((nextDestination - transform.position).normalized * movementSpeed * Time.deltaTime);
            }
        }
    } 

    public void TakeDamage(float damageAmt)
    {
        health -= damageAmt;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        PlayerStats.money += (int)maxHealth/10;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PathBlocker>())
        {
            collision.gameObject.GetComponent<PathBlocker>().SendMessage("TakeDamage", damageAmt);
            lastPunchTime = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PathBlocker>())
        {
            if (Time.time - lastPunchTime > reloadTime)
            {
                collision.gameObject.GetComponent<EnemyMovement>().SendMessage("TakeDamage", damageAmt);
                lastPunchTime = Time.time;
            }
        }
    }
}
