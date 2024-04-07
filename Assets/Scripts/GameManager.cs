using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private SolitareCard[] _shuffledDeck;
    private SolitareCard[] _drawDeck; //Follows solitare logic
    private SolitareRow[] _cardRows;
    private byte[] _stackedCardCount; //Basically counts how many cards are in each pile at the start
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
