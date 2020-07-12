using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public int towerType;
    public int locInHand;
    public int cost;
    public float[] probabilities;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        GameObject.FindObjectOfType<DeckManager>().PlayCard(this);    
    }

}
