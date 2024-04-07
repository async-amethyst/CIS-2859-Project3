using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolitareRow : MonoBehaviour
{
    private SolitareCard _currentBottomCard; // Used for parenting when new cards are dropped in the row.
    private List<SolitareCard> _flippedCards; // List of all cards that WILL be in the row, once flipped.
    private List<SolitareCard> _rowCards;

    public void AttemptDropCard(SolitareCard card)
    {
        if(SolitareCard.StackCardCheck(_currentBottomCard, card) && _rowCards.Count + card.ChildCount + 1 <= 7)
        {
            card.transform.parent = _currentBottomCard.transform;
            card.transform.localPosition = new Vector2(0, GameManager.ChildCardOffset);
            Transform parent = card.transform;
            while(parent.GetChild(0))
            {
                parent = parent.GetChild(0);
                _rowCards.Add(parent.GetComponent<SolitareCard>());
            }
            _currentBottomCard = card;
        }
        else { card.ResetCard(); }
    }
    /*
     * Intended behavior:
     * 1) When the "create" event is called, store data for what cards are in the row and create dummy cards.
     * 2) Create a single REAL card, which should have the ability to be picked up and such.
     * 3) Register when a card is removed from the row, removing it from the list and flipping over the next unflipped card (if there is one).
     * 4) Help register parents when a card is picked up BEHIND the front one.
     */
}
