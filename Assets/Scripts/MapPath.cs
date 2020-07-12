using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapPath : MonoBehaviour
{
    public GameObject pathTile;
    public GameObject towerTile;
    public float scale;
    public GameObject[] pathNodes;
    public ArrayList blocksAtADistance;

    // Start is called before the first frame update
    void Start()
    {
        ArrayList pathLocation = new ArrayList();
        List<GameObject> towerTiles = new List<GameObject>();
        List<GameObject> pathTiles = new List<GameObject>();
        for(int i = 0; i < pathNodes.Length - 1; i++)
        {
            for(int j = 0; j < (pathNodes[i].transform.position - pathNodes[i + 1].transform.position).magnitude/scale; j++)
            {
                Vector3 direction = (pathNodes[i + 1].transform.position - pathNodes[i].transform.position).normalized;
                pathTiles.Add(Instantiate(pathTile, pathNodes[i].transform.position + direction * scale * j, Quaternion.identity));
                (int, int) currLoc = ((int)(pathNodes[i].transform.position + direction * scale * j).x, (int)(pathNodes[i].transform.position + direction * scale * j).y);
                pathLocation.Add(currLoc); 
            }
        }

        for(float i = -7f; i <= 9.5f; i += 0.75f)
        {
            for (float j = -5f; j <= 5f; j += 0.75f)
            {
                (float, float) currLoc = (i, j);
                if (!pathLocation.Contains(currLoc))
                {
                    towerTiles.Add(Instantiate(towerTile, new Vector3(i, j, 0), Quaternion.Euler(0, 0, 0)));
                }
            }
        }

        GameObject[] towerTilesArr = (GameObject[]) towerTiles.ToArray();
        GameObject[] pathTilesArr = (GameObject[]) pathTiles.ToArray();
        blocksAtADistance = new ArrayList();
        blocksAtADistance.Add(new ArrayList());
        blocksAtADistance.Add(new ArrayList());
        blocksAtADistance.Add(new ArrayList());
        blocksAtADistance.Add(new ArrayList());
        blocksAtADistance.Add(new ArrayList());
        blocksAtADistance.Add(new ArrayList());
        blocksAtADistance.Add(new ArrayList());
        for (int i = 0; i < towerTilesArr.Length; i++)
        {
            GameObject currTile = towerTilesArr[i];
            float closestDistance = float.MaxValue;
            for (int j = 0; j < pathTilesArr.Length; j++)
            {
                GameObject currPathTile = pathTilesArr[j];
                float currDistance = (currPathTile.transform.position - currTile.transform.position).magnitude;
                if (currDistance < closestDistance)
                {
                    closestDistance = currDistance;
                }
            }
            ((ArrayList)blocksAtADistance[(int)closestDistance]).Add(currTile);
        }
    }
}