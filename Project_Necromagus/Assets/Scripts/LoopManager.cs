using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public Timer Timer;
    public CheckpointManager CheckpointManager;
    public PlayerControls PlayerControls;
    public PlayerSwapManager PlayerSwapManager;
    public TextMeshProUGUI RestartLoopText;

    private void Awake()
    {
        PlayerControls = PlayerControls.Instance;
    }

    private void Update()
    {
        Debug.Log(PlayerControls.RespawnKey());
        if (Timer.timer <= 0)
        {
            PlayerDie();
        }
        else if (PlayerControls.RespawnKey())
        {            
            Timer.timer = 0;
        }
    }

    private void PlayerDie()
    {
        PlayerControls.lockPlayer = true;
        RestartLoopText.gameObject.SetActive(true);
        if (PlayerControls.RespawnKey())
        {
            PlayerControls.respawnLock = true;
            RestartLoop();
        }
    }

    private void RestartLoop()
    {
        //NEED A COROUTINE
        Time.timeScale = 0;
        Timer.OnLoopStart();
        RestartLoopText.gameObject.SetActive(false);
        PlayerSwapManager.IncreaseBodyTimeLoopCount();
        PlayerSwapManager.CreateNewBody();
        CheckpointManager.OnLoopStart();
        PlayerSwapManager.RemoveLeftOverObjects();
        PlayerControls.lockPlayer = false;
        Time.timeScale = 1;
        PlayerControls.respawnLock = false;
        PlayerControls.TimeLoopCount++;
    }


}
