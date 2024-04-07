using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    private List<Card> cards;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void UpdateCardList(Card movedCard, Row targetRow)
    {
        var movedCardIndex = cards.IndexOf(movedCard);
        if(movedCardIndex == 0)
        {
            foreach(var card in cards)
            {
                targetRow.AddCard(card);
                cards.Remove(card);
            }
            return;
        }
        while (cards[movedCardIndex])
        {
            targetRow.AddCard(cards[movedCardIndex]);
            cards.Remove(cards[movedCardIndex]);
        }
    }

    public bool CanBeDropped(Card cardToDrop)
    {
        if ((cardToDrop.Color == cards[cards.Count - 1].Color) || (cardToDrop.Number != cards[cards.Count - 1].Number - 1)) return false;
        return true;
    }

    public void DropCard(Card cardToDrop)
    {
        if(cardToDrop.IsOnAceStack)
        {

        }
        cardToDrop.CardRow.UpdateCardList(cardToDrop, this);
        cardToDrop.transform.parent = cards[cards.Count - 1].transform;
        // set position so that they are aligned
    }
    
    private void MoveFromAceStack(Card cardToMove)
    {
        GameManager.Instance.GetAceStack(cardToMove.Suit).OnCardRemoved(cardToMove);
        cardToMove.SetCardRow(this);
        cardToMove.transform.SetParent(cards[cards.Count - 1].transform);
        cards.Add(cardToMove);
        // set local transform
    }

    public void AddCard(Card cardToAdd) { cards.Add(cardToAdd); }
    public byte GetCardChildCount(Card cardToCheck)
    {
        var index = cards.IndexOf(cardToCheck);
        return (byte)(cards.Count - index);
    }
}
