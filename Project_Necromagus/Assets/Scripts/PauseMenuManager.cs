using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    private PlayerControls _playerControls;

    private bool _endCondition;
    private bool _coroutineRunning;
    
    public GameObject PauseMenuGameObject;
    

    private void Start()
    {
        _playerControls = PlayerControls.Instance;    
    }

    private void Update()
    {
        UIMenuOnEscape();
    }

    private void UIMenuOnEscape()
    {
        if (_playerControls.GetEscape())
        {
            if (!_endCondition)
            {
                if (!_coroutineRunning)
                {
                    StartCoroutine(CheckCoroutine());
                }
            }
        }
    }
    
    private IEnumerator CheckCoroutine()
    {
        _coroutineRunning = true;
        ShowMenu();
        _playerControls.lockPlayer = true;
        Time.timeScale = 0;
        yield return new WaitWhile(() => _playerControls.GetEscape());
    }

    private void ShowMenu()
    {
        PauseMenuGameObject.SetActive(true);
    }

    private void CloseMenu()
    {
        PauseMenuGameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _playerControls.lockPlayer = false;
        CloseMenu();
        _coroutineRunning = false;
        StopCoroutine(CheckCoroutine());
    }
    
}
