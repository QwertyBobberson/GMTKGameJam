using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Texture2D map;
    [SerializeField] ColorToObjectMap colorToObjectMap;

    private void Start()
    {
        for(int x = 0; x < map.width; x++)
        {
            for(int y = 0; y < map.height; y++)
            {
            }
        }
    }
}
