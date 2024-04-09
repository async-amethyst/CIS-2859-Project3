using Solitare.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    private List<string> _cards = new List<string>();
    private List<Card> _cardList = new List<Card>();
    private GameObject _cardObjects;

    public List<Card> CardList => _cardList;
    public Card GetBottomCard() => _cardList[_cardList.Count - 1];
    public bool IsEmpty => _cardList.Count == 0;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddCard(string val) 
    { 
        _cards.Add(val); 
    }

    public void AddCardToList(Card card)
    {
        _cardList.Add(card);
        card.Sprite.sortingOrder = _cardList.IndexOf(card);
    }

    public void AddCardObj(Card cardToAdd)
    {
        if(_cardList.Count > 0)
        {
            var finalCard = _cardList[_cardList.Count - 1];
            _cardList.Add(cardToAdd);
            if (finalCard)
            {
                cardToAdd.transform.parent = finalCard.transform;
                cardToAdd.transform.localPosition = new Vector3(0, Card.CardOffset, -1 * _cardList.IndexOf(cardToAdd));
                cardToAdd.Sprite.sortingOrder = _cardList.IndexOf(cardToAdd);
            }
            cardToAdd.transform.localScale = new Vector2(1f, 1f);
            return;
        }
        else if(cardToAdd.Number == 13)
        {
            _cardList.Add(cardToAdd);
            cardToAdd.transform.parent = this.transform;
            cardToAdd.transform.localPosition = Vector2.zero;
            cardToAdd.Sprite.sortingOrder = 0;
            return;
        }
        _cardList.Add(cardToAdd);
        cardToAdd.transform.localScale *= 0.8333f;

        //Transform tempTrans = null;
        //tempTrans = cardToAdd.transform.GetChild(0);
        /*while(tempTrans)
        {
            _cardList.Add(tempTrans.GetComponent<Card>());
            tempTrans = tempTrans.GetChild(0);
        }*/
    }

    public void AddKingCard(Card kingCard)
    {
        _cardList.Add(kingCard);
        kingCard.Sprite.sortingOrder = _cardList.IndexOf(kingCard);
    }

    public void TryFlipFinalCard()
    {
        if (_cardList[_cardList.Count - 1])
        {
            _cardList[_cardList.Count - 1].FlipCard();
        }
    }

    public void RemoveCard(Card cardToRemove, Row targetRow)
    {
        var index = _cardList.IndexOf(cardToRemove);
        _cardList.Remove(cardToRemove);
        cardToRemove.SetCardRow(targetRow);
        targetRow.AddCardObj(cardToRemove);
        for(int i = index; i < _cardList.Count;) //By adding this here we don't have to do AddComponent and all that shit
        {
            var card = _cardList[i];
            _cardList.Remove(card);
            card.SetCardRow(targetRow);
            targetRow.AddCardToList(card);
        }
        if(_cardList.Count > 0 && !_cardList[_cardList.Count - 1].Grabbable)
        {
            _cardList[_cardList.Count - 1].FlipCard();
        }
    }

    public void RemoveCard(Card cardToRemove)
    {
        if (_cardList.IndexOf(cardToRemove) == -1) return;
        _cardList.Remove(cardToRemove);
        cardToRemove.SetCardRow(null);
        if (_cardList.Count > 0 && !_cardList[_cardList.Count - 1].Grabbable)
        {
            _cardList[_cardList.Count - 1].FlipCard();
        }
    }

    public void InstantiateCardObjects()
    {
        Transform parent = null;
        foreach(var card in _cards)
        {
            var cardObj = Instantiate(GameManager.Instance.CardPrefab);
            cardObj.transform.parent = this.transform;
            cardObj.transform.localPosition = Vector2.zero;
            if (parent != null) { cardObj.transform.parent = parent; cardObj.transform.localPosition = new Vector3(0, Card.CardOffset, _cards.IndexOf(card) * -1); }
            var cardScript = cardObj.GetComponent<Card>();
            cardScript.SetCardValue(card);
            cardScript.SetCardRow(this);
            _cardList.Add(cardScript);
            cardScript._cardSprite.sortingOrder = _cardList.IndexOf(cardScript);
            parent = cardObj.transform;
            if(card == _cards[_cards.Count - 1]) // If it's the last card in the row...
            {
                cardScript.FlipCard();
            }
        }
    }

    public void ClearCards()
    {
        foreach(var card in _cardList)
        {
            Destroy(card.gameObject);
        }
        _cardList.Clear();
        _cards.Clear();
    }
}
