using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamager : MonoBehaviour
{
    public int damageAmt;
    public int health;

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
            collision.gameObject.GetComponent<EnemyMovement>().SendMessage("TakeDamage", damageAmt * Time.fixedDeltaTime);
            Debug.Log(damageAmt * Time.fixedTime);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyMovement>())
        {
             collision.gameObject.GetComponent<EnemyMovement>().SendMessage("TakeDamage", damageAmt * Time.fixedDeltaTime);
            Debug.Log(damageAmt * Time.fixedTime);
        }
    }
}
