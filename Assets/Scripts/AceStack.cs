using Solitare.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceStack : MonoBehaviour
{
    private CardSuite _stackSuite;
    private byte _nextCardNumber;
    private SpriteRenderer _fakeCardSprite;

    void Start()
    {
        _fakeCardSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public bool CardValidityCheck(SolitareCard cardToCheck)
    {
        if(cardToCheck.Number == _nextCardNumber && cardToCheck.Suit == _stackSuite)
        {
            return true;
        }
        return false;
    }

    public bool OnCardDropped(SolitareCard card)
    {
        /* Intended behaviour -
         * 1 - Check to make sure the card is the same suit and number that is required.
         * 2 - Take the card from the user's hand, destroy it, and swap the sprite of the "placeholder" card. DONE
         * This way, we don't have to keep the triggers on top of the stack. We just keep this one Trigger.
         */
        if (!CardValidityCheck(card)) return false;
        _fakeCardSprite.sprite = CardSpriteHolder.Instance.GetCardSprite(card.Suit, card.Number);
        Destroy(card.gameObject);
        return true;
    }
}
