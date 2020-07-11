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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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
    }

    public void DrawCard()
    {
        for(int i = 0; i < hand.Length; i++)
        {
            if(hand[i] == null)
            {
                hand[i] = drawPile[cardNum];
                cardNum++;
                //Have to do something visual here
            }
        }

        if(cardNum == drawPile.Length - 1)
        {
            ShuffleIntoDrawPile(discardPile);
            for(int i = 0; i < discardPile.Count(); i++)
            {
                discardPile[i] = null;
                cardNum = 0;
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
