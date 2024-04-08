using Solitare.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private AceStack[] _aceStacks;
    [SerializeField] private Row[] _rows;
    public Card HoveredCard;
    public GameObject CardPrefab;
    private bool _isGrabbing;

    private List<string> cardPile = new List<string>();
    private List<string> shuffledDeck = new List<string>();
    [SerializeField] private List<CardReceiver> cardReceivers = new List<CardReceiver>();

    public Row HoveredRowToDrop;
    public AceStack HoveredAceStackToDrop;
    public List<string> CardPile => cardPile;

    private void Start()
    {
        GenerateGameStart();
    }
    private void Update()
    {

    }

    public void GenerateGameStart()
    {
        shuffledDeck.Clear();
        for(int i = 0; i < 4; i++)
        {
            for(int j = 1; j < 14; j++)
            {
                CardSuite suit = (CardSuite)i;
                shuffledDeck.Add(Enum.GetName(typeof(CardSuite), suit) + '|' + j.ToString());
            }
        }
        System.Random rand = new System.Random();
        ShuffleList();
        int desiredGoal = 1; // Raise this with each iteration, until we get 7 things of cards
        int entryToAdd = 0;
        foreach(var row in _rows)
        {
            for(int i = 0; i < desiredGoal; i++)
            {
                row.AddCard(shuffledDeck[entryToAdd]);
                entryToAdd++;
            }
            desiredGoal++;
            row.InstantiateCardObjects();
        }
        for(int i = 28; i < shuffledDeck.Count; i++)
        {
            cardPile.Add(shuffledDeck[i]);
        }
        DrawPile.Instance.InstantiateAndPrepareDrawCards();
    }

    private void ShuffleList()
    {
        for(int i = shuffledDeck.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            string temp = shuffledDeck[i];
            shuffledDeck[i] = shuffledDeck[j];
            shuffledDeck[j] = temp;
        }
    }

    public void ActivateReceivers(Card inputCard)
    {
        foreach(var receiver in cardReceivers)
        {
            receiver.CheckShouldActivate(inputCard);
        }
    }

    public AceStack GetAceStack(CardSuite suit) => _aceStacks[(int)suit];

    /*TODO -
     * Finish Update() method, adding other checks to reset and such
     * Create generation methods to place everything
     * Create sprite holder, that I can set from the Editor through the use of my script
     * Create timer that will update through the course of the game
     * Lots of other shit, this will probably be a day or two late. Sucks. I really don't want a late grade.
    */
}
