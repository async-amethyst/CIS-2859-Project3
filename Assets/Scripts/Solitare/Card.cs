using Solitare.Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public const float CardOffset = -0.1f;
    public const char CardDelimiter = '|';
    private CardSuite _suit;
    private byte _number;
    private CardColor _color;
    private bool _flipped;
    private bool _canBeGrabbed;
    public SpriteRenderer _cardSprite;
    private byte _childCount;
    private bool _isOnAceStack;
    private Row _cardRow;

    // Getters
    public CardSuite Suit => _suit;
    public byte Number => _number;
    public CardColor Color => _color;
    public byte CardCount => _childCount;
    public Row CardRow => _cardRow;
    public bool Grabbable => _canBeGrabbed;
    public SpriteRenderer Sprite => _cardSprite;
    public bool IsOnAceStack
    {
        get { return _isOnAceStack; }
        set { _isOnAceStack = value; }
    }
    public void SetCardRow(Row rowToSet) { _cardRow = rowToSet; }
    void Start()
    {
        _cardSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public void SetCardValue(string valToSet)
    {
        string[] str = valToSet.Split(CardDelimiter);
        _suit = GetSuitFromStr(str[0]);
        _number = (byte)int.Parse(str[1]);
        SetCardColor();
    }

    public void FlipCard()
    {
        _cardSprite = GetComponent<SpriteRenderer>();
        _cardSprite.sprite = GetCardSprite();
        _canBeGrabbed = true;
    }

    public static CardSuite GetSuitFromStr(string str)
    {
        switch(str)
        {
            case "Diamond":
                return CardSuite.Diamond;
            case "Spade":
                return CardSuite.Spade;
            case "Heart":
                return CardSuite.Heart;
            case "Club":
                return CardSuite.Club;
        }
        return CardSuite.Diamond;
    }

    public bool CanStack(Card cardToCompare)
    {
        return (cardToCompare.Color != this.Color && cardToCompare.Number == this.Number - 1);
    }

    private Sprite GetCardSprite()
    {
        var index = (((int)_suit * 13) + _number - 1);
        return SpriteManager.Instance.AllCardSprites[index];
    }

    private void SetCardColor()
    {
        _color = (_suit == CardSuite.Spade || _suit == CardSuite.Club) ? CardColor.Black : CardColor.Red;
    }

    private void OnMouseEnter()
    {
        PlayerInputManager.Instance.CurrentHoveredCard = this;
    }

    private void OnMouseExit()
    {
        PlayerInputManager.Instance.CurrentHoveredCard = null;
    }

    /* TODO -
     * Add reset method so that the card can be reset if not eligible
    */
}
