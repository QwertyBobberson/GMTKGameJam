using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyMovement : MonoBehaviour
{
    GameObject[] pathNodes;
    public Sprite onFireEffect;
    public Sprite onWaterEffect;
    public Sprite defaultSprite;
    int nextIndex = 0;
    Vector3 nextDestination;
    public float movementSpeed;
    public float maxHealth;
    public float health;
    public float damageAmt;
    public float reloadTime;
    public float lastPunchTime;
    public float waterSlowdownRatio;
    public float onFireDamageAmt;
    float originalSpeed;

    bool dontMove = false;
    

    public float fireTimeRemaining = 0.0f;
    public float waterTimeRemaining = 0.0f;

    GameObject fireSurrounder = null;
    GameObject waterSurrounder = null;

    public float timeOfLastDamageFromBlocker = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        pathNodes = GameObject.FindObjectOfType<MapPath>().pathNodes;
        timeOfLastDamageFromBlocker = 0.0f;
        originalSpeed = movementSpeed;
        lastPunchTime = 0.0f;
        nextDestination = pathNodes[0].transform.position;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (fireTimeRemaining > 0.0f)
        {
            if (GetComponent<SpriteRenderer>().sprite != onFireEffect)
            {
                GetComponent<SpriteRenderer>().sprite = onFireEffect;
                TakeDamage(onFireDamageAmt * Time.deltaTime);
            }
        } else if (GetComponent<SpriteRenderer>().sprite == onFireEffect)
        {
            GetComponent<SpriteRenderer>().sprite = defaultSprite;
        }

        if (waterTimeRemaining > 0.0f)
        {
            if (GetComponent<SpriteRenderer>().sprite != onWaterEffect)
            {
                GetComponent<SpriteRenderer>().sprite = onWaterEffect;
                movementSpeed = originalSpeed * waterSlowdownRatio;
            }
        }
        else if (GetComponent<SpriteRenderer>().sprite == onWaterEffect)
        {
            movementSpeed = originalSpeed;
            GetComponent<SpriteRenderer>().sprite = defaultSprite;
        }
        fireTimeRemaining -= Time.deltaTime;
        waterTimeRemaining -= Time.deltaTime;
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
