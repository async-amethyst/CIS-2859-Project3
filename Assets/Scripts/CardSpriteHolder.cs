using Solitare.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpriteHolder : MonoSingleton<CardSpriteHolder>
{
    [SerializeField] private Texture2D _cardSpriteSheet;
    private Sprite[] _cardSprites;

    public Sprite GetCardSprite(CardSuite suit, byte index) => _cardSprites[(byte)suit == 0 ? (14*(byte)suit) + index : (14*(byte)suit) + 1 + index]; //Convuluted, yes, but it's what I have to do in order to make it load the right stuff.
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
