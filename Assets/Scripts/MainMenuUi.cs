using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField] private Transform _difficultySelectBackground;
    [SerializeField] private Transform _easyButtonBackground;
    [SerializeField] private Transform _hardButtonBackground;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _resetButton;
    public void SwapDifficulty()
    {
        Vector3 targetPosition = GameManager.Instance.Difficulty == 2 ? _easyButtonBackground.position : _hardButtonBackground.position;
        StartCoroutine(UiAnimation.LerpElement(_difficultySelectBackground, targetPosition, 0.1f));
        if (GameManager.Instance.Difficulty == 1) GameManager.Instance.Difficulty = 2;
        else GameManager.Instance.Difficulty = 1;
    }

    public void StartGame()
    {
        GameManager.CallGameStart.Invoke();
        gameObject.SetActive(false);
        _resetButton.SetActive(true);
    }

    private void ActivateWin()
    {
        _winScreen.SetActive(true);
    }

    private void Start()
    {
        GameManager.OnGameWin += ActivateWin;
    }
}
