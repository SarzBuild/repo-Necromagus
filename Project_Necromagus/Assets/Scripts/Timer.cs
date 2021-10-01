using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    public float timer;
    public int currentMaxTime;
    
    
    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        OnLoopStart();
    }

    public void OnLoopStart()
    {
        timer = currentMaxTime;
    }
    
    private void Update()
    {
        Countdown();
    }

    private void Countdown()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("00");
        }
    }
    
}
