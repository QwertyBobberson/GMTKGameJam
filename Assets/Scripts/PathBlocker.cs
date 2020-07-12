using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBlocker : MonoBehaviour
{

    public int damageAmt;
    public int health;
    public float reloadTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyMovement>())
        {
            collision.gameObject.GetComponent<EnemyMovement>().SendMessage("TakeDamage", damageAmt);
            collision.gameObject.GetComponent<EnemyMovement>().timeOfLastDamageFromBlocker = Time.time;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyMovement>())
        {
            if (Time.time - collision.gameObject.GetComponent<EnemyMovement>().timeOfLastDamageFromBlocker > reloadTime)
            {
                collision.gameObject.GetComponent<EnemyMovement>().SendMessage("TakeDamage", damageAmt);
                collision.gameObject.GetComponent<EnemyMovement>().timeOfLastDamageFromBlocker = Time.time;
            }
        }
    }

    public void TakeDamage(int damageAmt)
    {
        health -= damageAmt;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
