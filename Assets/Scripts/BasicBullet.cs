using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{

    public int damageAmt;

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
        Debug.Log("hm");
        if (collision.gameObject.GetComponent<EnemyMovement>())
        {
            collision.gameObject.GetComponent<EnemyMovement>().SendMessage("TakeDamage", damageAmt);
            Destroy(gameObject);
        }
    }
}
