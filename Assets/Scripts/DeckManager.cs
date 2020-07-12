using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<GameObject> allTowers;
    public List<GameObject> deck = new List<GameObject>();
    public GameObject[] hand;
    public List<GameObject> discardPile;
    public List<GameObject> drawPile;

    public int cardNum;
    public Vector3 cardLocation;
    public Vector3 cardOffset;

    public GameObject[] towerTypes;

    List<ArrayList> blocksAtADistance;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        blocksAtADistance = GameObject.FindObjectOfType<MapPath>().blocksAtADistance;
        hand = new GameObject[3];

        while(allTowers.Count != 0)
        {
            int i = Random.Range(0, allTowers.Count);
            drawPile.Add(GameObject.Instantiate(allTowers[i], new Vector3(100, 100, 0), Quaternion.identity));
            allTowers.RemoveAt(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            DrawCard();
        }
    }

    public void ShuffleIntoDrawPile(List<GameObject> toShuffle)
    {
        while (toShuffle.Count != 0)
        {
            int i = Random.Range(0, toShuffle.Count);
            drawPile.Add(toShuffle[i]);
            toShuffle.RemoveAt(i);
        }
    }

    public void DrawCard()
    {
        if(drawPile.Count == 0)
        {
            ShuffleIntoDrawPile(discardPile);
        }

        for (int i = 0; i < hand.Length; i++)
        {
            if(hand[i] == null)
            {
                hand[i] = drawPile[drawPile.Count - 1];
                drawPile.RemoveAt(drawPile.Count - 1);
                hand[i].GetComponent<Card>().locInHand = i;
                hand[i].transform.position = cardLocation + cardOffset * i;
                //Have to do something visual here
                return;
            }
        }
    }

    public void PlayCard(Card card)
    {
        int i = card.locInHand;
        // arbitrarily picking a distance of 4 for now:
        SpawnCard(4, card);
        discardPile.Add(card.gameObject);
        card.transform.position = new Vector3(100, 100, 0);
        hand[i] = null;
        DrawCard();
    }

    public void SpawnCard(int distance, Card card)
    {
        distance--;

        int randomSpawnPoint = Random.Range(0, blocksAtADistance[distance].Count);
        Vector3 spawnLocation = ((GameObject)blocksAtADistance[distance][randomSpawnPoint]).transform.position;
        GameObject.Instantiate(towerTypes[card.GetComponent<Card>().towerType], spawnLocation, Quaternion.Euler(0,0,0));

    }
}
