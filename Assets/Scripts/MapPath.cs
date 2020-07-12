using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MapPath : MonoBehaviour
{
    public GameObject pathTile;
    public GameObject towerTile;
    public float scale;
    public GameObject[] pathNodes;
    public List<GameObject> pathTiles;
    public List<ArrayList> blocksAtADistance;

    // Start is called before the first frame update
    void Awake()
    {
        List<Vector2> pathLocation = new List<Vector2>();
        List<GameObject> towerTiles = new List<GameObject>();
        pathTiles = new List<GameObject>();
        for(int i = 0; i < pathNodes.Length - 1; i++)
        {
            for(int j = 0; j < (pathNodes[i].transform.position - pathNodes[i + 1].transform.position).magnitude/scale; j++)
            {
                Vector3 direction = (pathNodes[i + 1].transform.position - pathNodes[i].transform.position).normalized;
                pathTiles.Add(Instantiate(pathTile, pathNodes[i].transform.position + direction * scale * j, Quaternion.identity));
                Vector2 currLoc = (pathNodes[i].transform.position + direction * scale * j);
                pathLocation.Add(currLoc); 
            }
        }

        pathTiles.Add(Instantiate(pathTile, pathNodes[pathNodes.Count() - 1].transform.position, Quaternion.identity));
        pathLocation.Add(pathNodes[pathNodes.Count() - 1].transform.position);


        for (float i = -7f; i <= 9.5f; i += 0.75f)
        {
            for (float j = -5f; j <= 5f; j += 0.75f)
            {
                Vector2 currLoc = new Vector2(i, j);
                bool defNot = false;
                for (int k = 0; k < pathLocation.Count; k++)
                {
                    if ((pathLocation[k] - currLoc).sqrMagnitude < 0.3)
                    {
                        defNot = true;
                    }
                }
               if (!defNot)
               {
                    // def yes
                    towerTiles.Add(Instantiate(towerTile, new Vector3(i, j, 0), Quaternion.Euler(0, 0, 0)));
               }
            }
        }

        GameObject[] towerTilesArr = (GameObject[]) towerTiles.ToArray();
        GameObject[] pathTilesArr = (GameObject[]) pathTiles.ToArray();
        blocksAtADistance = new List<ArrayList>();
        for (int  i = 0; i < 13; i++)
        {
            blocksAtADistance.Add(new ArrayList());
        }
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
            int indexInArr = Mathf.RoundToInt(closestDistance / 0.75f) - 1;
            ((ArrayList)blocksAtADistance[indexInArr]).Add(currTile);
            Debug.Log( indexInArr + " " + blocksAtADistance[indexInArr].Count);
        }
    }
}