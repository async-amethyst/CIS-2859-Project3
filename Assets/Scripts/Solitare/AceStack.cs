using Solitare.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceStack : MonoBehaviour
{
    private List<Card> _cardsOnStack;
    private byte _nextCardNumber;
    private CardSuite _stackSuit;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnCardRaised()
    {
        var cardToActivate = _cardsOnStack[_cardsOnStack.Count - 2];
        if(cardToActivate) { cardToActivate.gameObject.SetActive(true); }
    }

    public void OnCardRemoved(Card cardToRemove)
    {
        _cardsOnStack.Remove(cardToRemove);
        _nextCardNumber = (byte)_cardsOnStack.Count;
    }

    public void OnCardReturned()
    {
        var cardToDeactivate = _cardsOnStack[_cardsOnStack.Count - 2];
        if (cardToDeactivate) { cardToDeactivate.gameObject.SetActive(false); }
    }

    public void OnCardAdd(Card cardToAdd)
    {
        _cardsOnStack.Add(cardToAdd);
        cardToAdd.transform.position = this.transform.position;
        cardToAdd.transform.parent = null;
        var cardToDeactivate = _cardsOnStack[_cardsOnStack.Count - 2];
        if (cardToDeactivate) { cardToDeactivate.gameObject.SetActive(false); }
        _nextCardNumber = (byte)_cardsOnStack.Count;
    }

    public bool CanDropCard(Card cardToDrop)
    {
        return (cardToDrop.Suit == _stackSuit) && (cardToDrop.Number == _nextCardNumber);
    }
}
