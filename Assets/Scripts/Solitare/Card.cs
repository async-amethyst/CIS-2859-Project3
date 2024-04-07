using Solitare.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private CardSuite _suit;
    private byte _number;
    private CardColor _color;
    private bool _flipped;
    private bool _canBeGrabbed;
    private SpriteRenderer _cardSprite;
    private byte _childCount;
    private bool _isOnAceStack;
    private Row _cardRow;

    // Getters
    public CardSuite Suit => _suit;
    public byte Number => _number;
    public CardColor Color => _color;
    public byte CardCount => _childCount;
    public Row CardRow => _cardRow;
    public bool IsOnAceStack => _isOnAceStack;
    void Start()
    {
        _color = (_suit == CardSuite.Spade || _suit == CardSuite.Club) ? CardColor.Black : CardColor.Red;
        _cardSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public void FlipCard()
    {
        //_cardSprite.sprite == GameManager.FlippedCardSprite;
        _canBeGrabbed = true;
    }

    private void OnCardGrab()
    {
        if(!_isOnAceStack) _childCount = _cardRow.GetCardChildCount(this); return;
                
    }

    public void SetCardRow(Row rowToSet)
    {
        _cardRow = rowToSet;
    }

    /* TODO -
     * Add reset method so that the card can be reset if not eligible
    */
}
