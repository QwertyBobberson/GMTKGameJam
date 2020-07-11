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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        hand = new GameObject[3];
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
                cardNum++;
                Instantiate(hand[i], cardLocation + cardOffset * i, Quaternion.identity);
                //Have to do something visual here
                return;
            }
        }
    }

    public void PlayCard(GameObject card)
    {
        for(int i = 0; i < hand.Length; i++)
        {
            if(hand[i] == card)
            {
                SpawnCard();
                discardPile[i] = card;
                hand[i] = null;
                DrawCard();
            }
        }
    }

    public void SpawnCard()
    {
        //Someone else can do this
    }
}
