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
    List<GameObject> pathTiles;
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
        pathTiles = GameObject.FindObjectOfType<MapPath>().pathTiles;
        for(int i = 0; i < hand.Length; i++)
        {
            DrawCard();
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
        if(card.cost <= PlayerStats.money)
        {
            PlayerStats.money -= card.cost;
            int i = card.locInHand;
            // arbitrarily picking a distance of 4 for now:
            discardPile.Add(card.gameObject);
            card.transform.position = new Vector3(100, 100, 0);
            int randDistance = 0;
            int counter = 0;
            do
            {
                randDistance = 0;
                counter++;
                float randPercent = Random.Range(0.0f, 1.0f);
                Debug.Log("rand percent:" + randPercent + " " + card.probabilities.Length);
                float sumOfProbabilities = 0.0f;
                while (randPercent > sumOfProbabilities && randDistance < card.probabilities.Length)
                {
                    sumOfProbabilities += card.probabilities[randDistance];
                    Debug.Log("sum:" + sumOfProbabilities);
                    randDistance++;
                }
                randDistance--;
                Debug.Log("dist:" + randDistance);
            } while (blocksAtADistance[randDistance].Count < 1 && counter < 1000);
            SpawnCard(randDistance, card);
            hand[i] = null;
            DrawCard();
        }
    }

    public void SpawnCard(int distance, Card card)
    {
        int randomSpawnPoint = Random.Range(0, blocksAtADistance[distance].Count);
        Vector3 spawnLocation = ((GameObject)blocksAtADistance[distance][randomSpawnPoint]).transform.position;
        if (card.GetComponent<Card>().towerType == 1)
        {
            randomSpawnPoint = Random.Range(0, pathTiles.Count);
            spawnLocation = ((GameObject)pathTiles[randomSpawnPoint]).transform.position;

        }
        GameObject.Instantiate(towerTypes[card.GetComponent<Card>().towerType], spawnLocation, Quaternion.Euler(0, 0, 0));
    }
}
