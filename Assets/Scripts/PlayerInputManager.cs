using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoSingleton<PlayerInputManager>
{
    private const float ConstClickCooldown = 0.5f; // 500 ms click cooldown
    private bool playerClicking => Input.GetMouseButton(0);
    private bool _cardCurrentlySelected;
    private Card _currentlySelectedCard;
    private float _playerClickCooldown;
    private bool _onCooldown;

    [SerializeField] public Card CurrentHoveredCard;
    [SerializeField] public CardReceiver CurrentHoveredReceiver;
    public bool CardSelected => _cardCurrentlySelected;
    public bool IsDeckHovered;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(_onCooldown) // Player input delay, make sure that they can't spam click shtuff
        {
            _playerClickCooldown += Time.deltaTime;
            if(_playerClickCooldown >= ConstClickCooldown)
            {
                _onCooldown = false;
                _playerClickCooldown = 0f;
            }
            return;
        }
        if(playerClicking && !_cardCurrentlySelected && CurrentHoveredCard && CurrentHoveredCard.Grabbable)
        {
            _currentlySelectedCard = CurrentHoveredCard;
            _cardCurrentlySelected = true;
            _currentlySelectedCard.transform.localScale *= 1.2f;
            GameManager.Instance.ActivateReceivers(_currentlySelectedCard);
            _onCooldown = true;
        }
        else if(playerClicking && _cardCurrentlySelected && CurrentHoveredReceiver)
        {
            CurrentHoveredReceiver.HandleCardDrop(_currentlySelectedCard);
            ResetVariables();
            _onCooldown = true;
        }
        else if(playerClicking && _cardCurrentlySelected && !CurrentHoveredReceiver && !CurrentHoveredCard)
        {
            _currentlySelectedCard.transform.localScale *= 0.833333f;
            ResetVariables();
            _onCooldown = true;
        }
        else if(playerClicking && IsDeckHovered)
        {
            DrawPile.Instance.DrawCard();
            _onCooldown = true;
        }
    }

    private void ResetVariables()
    {
        _currentlySelectedCard = null;
        _cardCurrentlySelected = false;
        CurrentHoveredCard = null;
        CurrentHoveredReceiver = null;
    }
}
