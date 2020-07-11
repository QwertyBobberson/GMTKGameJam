using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject[] pathNodes;
    int nextIndex = 0;
    Vector3 nextDestination;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        pathNodes = GameObject.FindObjectOfType<MapPath>().pathNodes;
        nextDestination = pathNodes[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((nextDestination - transform.position).sqrMagnitude < 0.0001)
        {
            if (nextIndex < pathNodes.Length - 1)
            {
                nextIndex++;
                nextDestination = pathNodes[nextIndex].transform.position;
            }
        }
        else
        {
            transform.Translate((nextDestination - transform.position).normalized * movementSpeed * Time.deltaTime);
        }
    }
}
