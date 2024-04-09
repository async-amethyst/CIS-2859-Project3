using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPile : MonoSingleton<DrawPile>
{
    public List<Card> cardList;
    private int _activeCardIndex;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InstantiateAndPrepareDrawCards()
    {
        foreach(var card in GameManager.Instance.CardPile)
        {
            var cardObj = Instantiate(GameManager.Instance.CardPrefab);
            cardObj.transform.parent = this.transform;
            cardObj.transform.localPosition = new Vector2(1.0f, 0f);
            var cardScript = cardObj.GetComponent<Card>();
            cardScript.SetCardValue(card);
            cardScript.FlipCard();
            cardList.Add(cardScript);
            cardObj.SetActive(false);
        }
    }

    public void DrawCard()
    {
        if(cardList.Count > _activeCardIndex) cardList[_activeCardIndex].gameObject.SetActive(false);
        if(GameManager.Instance.Difficulty == 1)
        {
            _activeCardIndex++;
        }
        else { _activeCardIndex += 3; }
        if (_activeCardIndex >= cardList.Count) _activeCardIndex = 0;
        cardList[_activeCardIndex].gameObject.SetActive(true);
    }

    private void OnMouseEnter()
    {
        PlayerInputManager.Instance.IsDeckHovered = true;
    }

    private void OnMouseExit()
    {
        PlayerInputManager.Instance.IsDeckHovered = false;
    }

    public void ClearCards()
    {
        foreach(var card in cardList)
        {
            Destroy(card.gameObject);
        }
        cardList.Clear();
    }
}
