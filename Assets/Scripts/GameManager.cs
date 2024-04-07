using Solitare.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private AceStack[] _aceStacks;
    public Card HoveredCard;
    private bool _isGrabbing;

    public Row HoveredRowToDrop;
    public AceStack HoveredAceStackToDrop;

    private void Update()
    {
        if(_isGrabbing)
        {
            if(!Input.GetMouseButton(0))
            {
                if(HoveredRowToDrop != null)
                {
                    if(HoveredRowToDrop.CanBeDropped(HoveredCard))
                    {
                        HoveredRowToDrop.DropCard(HoveredCard);
                    }
                }
                else if(HoveredAceStackToDrop != null)
                {
                    if(HoveredAceStackToDrop.CanDropCard(HoveredCard))
                    {
                        HoveredAceStackToDrop.OnCardAdd(HoveredCard);
                    }
                }
            }
        }
        if(HoveredCard != null && !_isGrabbing && Input.GetMouseButton(0))
        {

        }
    }
    public AceStack GetAceStack(CardSuite suit) => _aceStacks[(int)suit];

    /*TODO -
     * Finish Update() method, adding other checks to reset and such
     * Create generation methods to place everything
     * Create sprite holder, that I can set from the Editor through the use of my script
     * Create timer that will update through the course of the game
     * Lots of other shit, this will probably be a day or two late. Sucks. I really don't want a late grade.
    */
}
