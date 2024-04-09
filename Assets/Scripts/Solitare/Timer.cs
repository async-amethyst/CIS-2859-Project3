using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    private float _timerVal;
    private bool _timerRunning = false;
    void Start()
    {
        GameManager.OnGameWin += StopTimer;
        GameManager.CallGameStart += StartTimer;
        GameManager.OnGameRestart += ResetTimer;
    }

    void Update()
    {
        if(_timerRunning)
        {
            _timerVal += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(_timerVal);
            _timerText.text = time.ToString(@"mm\:ss\:fff");
        }
    }

    public void StartTimer()
    {
        _timerRunning = true;
    }

    public void StopTimer()
    {
        _timerRunning = false;
    }

    private void ResetTimer()
    {
        _timerVal = 0;
    }
}
