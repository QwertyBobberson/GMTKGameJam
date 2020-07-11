using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapPath : MonoBehaviour
{
    public GameObject pathTile;
    public float scale;
    public GameObject[] pathNodes;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < pathNodes.Length - 1; i++)
        {
            for(int j = 0; j < (pathNodes[i].transform.position - pathNodes[i + 1].transform.position).magnitude/scale; j++)
            {
                Vector3 direction = (pathNodes[i + 1].transform.position - pathNodes[i].transform.position).normalized;
                Instantiate(pathTile, pathNodes[i].transform.position + direction * scale * j, Quaternion.identity);
            }
        }
    }
}
