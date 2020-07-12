using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject[] allTowers;
    public List<GameObject> deck = new List<GameObject>();
    public GameObject[] hand;
    public GameObject[] discardPile;
    public GameObject[] drawPile;

    public int cardNum;
    public Vector3 cardLocation;
    public Vector3 cardOffset;

    public GameObject[] towerTypes;

    List<ArrayList> blocksAtADistance;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        hand = new GameObject[3];
        blocksAtADistance = GameObject.FindObjectOfType<MapPath>().blocksAtADistance;
        Debug.Log(blocksAtADistance);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            ShuffleIntoDrawPile(allTowers);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            DrawCard();
        }
    }

    public void ShuffleIntoDrawPile(GameObject[] toShuffle)
    {
        drawPile = new GameObject[toShuffle.Count()];

        for(int i = 0; i < toShuffle.Count(); i++)
        {
            int index;

            do
            {
                index = Random.Range(0, drawPile.Count());
            } while (drawPile[index] != null);

            drawPile[index] = toShuffle[i];
        }

        discardPile = new GameObject[drawPile.Count()];
        cardNum = 0;
    }

    public void DrawCard()
    {
        if (cardNum == drawPile.Length)
        {
            ShuffleIntoDrawPile(discardPile);
            for (int j = 0; j < discardPile.Count(); j++)
            {
                discardPile[j] = null;
            }
        }

        for (int i = 0; i < hand.Length; i++)
        {
            if(hand[i] == null)
            {
                hand[i] = drawPile[cardNum];
                hand[i].GetComponent<Card>().locInHand = i;
                cardNum++;
                Instantiate(hand[i], cardLocation + cardOffset * i, Quaternion.identity);
                //Have to do something visual here
                return;
            }
        }
    }

    public void PlayCard(Card card)
    {
        int i = card.locInHand;
        // arbitrarily picking a distance of 4 for now:
        Debug.Log("ooh that tickles like cheese");
        SpawnCard(4, card);
        discardPile[i] = card.gameObject;
        hand[i] = null;
        DrawCard();
    }

    public void SpawnCard(int distance, Card card)
    {
        distance--;
        Debug.Log(blocksAtADistance);
        Debug.Log(blocksAtADistance.Count);
        Debug.Log(blocksAtADistance[distance]);

        int randomSpawnPoint = Random.Range(0, blocksAtADistance[distance].Count);
        Vector3 spawnLocation = ((GameObject)blocksAtADistance[distance][randomSpawnPoint]).transform.position;
        GameObject.Instantiate(towerTypes[card.GetComponent<Card>().towerType], spawnLocation, Quaternion.Euler(0,0,0));
        Debug.Log("ooh that tickles burgers");

    }
}
