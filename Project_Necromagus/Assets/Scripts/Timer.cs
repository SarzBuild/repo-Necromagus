using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private float timer;
    public int currentMaxTime;
    
    
    void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timer = currentMaxTime;
    }
    void Update()
    {
        Countdown();
    }

    void Countdown()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("00");
        }
        else
        {
            
        }
    }
    
}
