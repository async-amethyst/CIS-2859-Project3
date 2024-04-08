using Solitare.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceStack : MonoBehaviour
{
    [SerializeField] private GameObject _dummySprite;
    private List<Card> _cardsOnStack = new List<Card>();
    private byte _nextCardNumber = 1;
    [SerializeField] private CardSuite _stackSuit;
    private SpriteRenderer _dummyCardRenderer;

    public CardSuite Suit => _stackSuit;
    public bool HasNoCards => _nextCardNumber == 1;
    public byte NextNumber => _nextCardNumber;
    public GameObject DummyCard => _dummySprite;
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
        var cardToActivate = _cardsOnStack[_cardsOnStack.Count - 1];
        if (cardToActivate) { cardToActivate.gameObject.SetActive(true); }
    }

    public void OnCardReturned()
    {
        var cardToDeactivate = _cardsOnStack[_cardsOnStack.Count - 2];
        if (cardToDeactivate) { cardToDeactivate.gameObject.SetActive(false); }
    }

    public void OnCardAdd(Card cardToAdd)
    {
        _cardsOnStack.Add(cardToAdd);
        cardToAdd.transform.parent = this.transform;
        cardToAdd.transform.localPosition = new Vector2(0, 0);
        cardToAdd.Sprite.rendererPriority = _nextCardNumber;
        cardToAdd.transform.localScale = Vector3.one;
        _nextCardNumber = (byte)(_cardsOnStack.Count + 1);
    }

    public bool CanDropCard(Card cardToDrop)
    {
        return (cardToDrop.Suit == _stackSuit) && (cardToDrop.Number == _nextCardNumber);
    }
}
