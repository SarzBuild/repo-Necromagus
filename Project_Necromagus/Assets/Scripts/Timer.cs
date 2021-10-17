using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private PlayerControls _playerControls;
    private bool _startTime;
    public float timer;
    public int currentMaxTime;
    
    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        OnLoopStart();
    }

    private void Start()
    {
        _playerControls = PlayerControls.Instance;
    }

    public void OnLoopStart()
    {
        timer = currentMaxTime;
    }
    
    private void FixedUpdate()
    {
        if (_playerControls.GetMovingRightSnap() == 1 || _playerControls.GetMovingLeftSnap() == 1 ||
            _playerControls.GetMovingUp())
        {
            _startTime = true;
        }
        if (_startTime)
        {
            Countdown();
        }
    }

    private void Countdown()
    {
        if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            timerText.text = timer.ToString("00");
        }
    }
    
}
