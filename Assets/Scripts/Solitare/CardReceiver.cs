using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CardReceiver : MonoBehaviour
{
    private Row _associatedRow; //The row that this is receiving for
    private AceStack _associatedAceStack;
    private BoxCollider2D _raycastReceiver;
    void Start()
    {
        _raycastReceiver = GetComponent<BoxCollider2D>();
        _associatedRow = transform.parent.GetComponent<Row>();
        _associatedAceStack = transform.parent.GetComponent<AceStack>();
    }

    void Update()
    {
        
    }

    public void CheckShouldActivate(Card grabbedCard)
    {
        UpdatePosition();
        if(_associatedAceStack)
        {
            if(grabbedCard.Suit == _associatedAceStack.Suit && grabbedCard.Number == _associatedAceStack.NextNumber) { _raycastReceiver.enabled = true; }
            else
            {
                _raycastReceiver.enabled = false;
            }
            return;
        }
        if(_associatedRow.IsEmpty && grabbedCard.Number == 13) { _raycastReceiver.enabled = true; return; }
        else if(_associatedRow.IsEmpty && grabbedCard.Number != 13) { _raycastReceiver.enabled = false; return; }
        if (_associatedRow.GetBottomCard().CanStack(grabbedCard))
        {
            _raycastReceiver.enabled = true;
        }
        else _raycastReceiver.enabled = false;
    }

    public void UpdatePosition()
    {
        if(_associatedRow)
        {
            transform.localPosition = new Vector2(0, _associatedRow.CardList.Count * Card.CardOffset * 10);
        }
    }

    public void HandleCardDrop(Card cardToDrop)
    {
        cardToDrop.transform.localScale = Vector3.one;
        cardToDrop.transform.parent = null;
        if (cardToDrop.CardRow == null && _associatedAceStack) //Card coming from a Row, and going on the ace stack.
        {
            cardToDrop.IsOnAceStack = true;
            _associatedAceStack.OnCardAdd(cardToDrop);
            DrawPile.Instance.cardList.Remove(cardToDrop);
            return;
        }
        else if (cardToDrop.CardRow == null && _associatedRow) // If card is going on a Row, and came from the pile
        {
            cardToDrop.SetCardRow(_associatedRow);
            _associatedRow.AddCardObj(cardToDrop);
            DrawPile.Instance.cardList.Remove(cardToDrop);
            return;
        }
        else if (cardToDrop.IsOnAceStack)
        {
            AceStack stack = GameManager.Instance.GetAceStack(cardToDrop.Suit);
            stack.OnCardRemoved(cardToDrop);
            cardToDrop.IsOnAceStack = false;
            cardToDrop.CardRow.RemoveCard(cardToDrop, _associatedRow);
            return;
        }
        else if(_associatedAceStack)
        {
            cardToDrop.IsOnAceStack = true;
            if (cardToDrop.CardRow) cardToDrop.CardRow.RemoveCard(cardToDrop);
            _associatedAceStack.OnCardAdd(cardToDrop);
            return;
        }
        cardToDrop.CardRow.RemoveCard(cardToDrop, _associatedRow);
    }

    private void OnMouseEnter()
    {
        PlayerInputManager.Instance.CurrentHoveredReceiver = this;
    }

    private void OnMouseExit()
    {
        PlayerInputManager.Instance.CurrentHoveredReceiver = null;
    }
}
