using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolitareRow : MonoBehaviour
{
    /*
     * Intended behavior:
     * 1) When the "create" event is called, store data for what cards are in the row and create dummy cards.
     * 2) Create a single REAL card, which should have the ability to be picked up and such.
     * 3) Register when a card is removed from the row, removing it from the list and flipping over the next unflipped card (if there is one).
     * 4) Help register parents when a card is picked up BEHIND the front one.
     */
}
