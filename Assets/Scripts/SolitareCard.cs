using Solitare.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolitareCard : MonoBehaviour
{
    private CardSuite _suit;
    private byte _number;
    private CardColor _color;
    private Vector2 _cardPreviousLocation;
    private bool _isBeingCarried;

    public GameObject _collidedTarget;

    public CardSuite Suit => _suit;
    public CardColor Color => _color;
    public byte Number => _number;

    public static bool StackCardCheck(SolitareCard cardOnStack, SolitareCard cardToStack) =>
        cardOnStack.Color != cardToStack.Color && cardOnStack.Number == cardToStack.Number - 1;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnCardPickup()
    {
        _cardPreviousLocation = transform.position;
    }

    public void AttemptCardDrop()
    {
        if(_collidedTarget == null)
        {
            ResetCard();
            return;
        }
        var _triggerAceStack = _collidedTarget.GetComponent<AceStack>();
        if(_triggerAceStack)
        {
            if(!_triggerAceStack.OnCardDropped(this))
            {
                ResetCard();
                return;
            }
        }
    }

    private void ResetCard()
    {
        transform.position = _cardPreviousLocation;
        _isBeingCarried = false;
    }
}
