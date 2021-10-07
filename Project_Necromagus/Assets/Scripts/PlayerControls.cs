using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Camera mainCamera;
    public bool lockPlayer;
    public bool cursorVisibility;
    public bool respawnLock;
    public int TimeLoopCount;
    private float _rv;
    private float _lv;
    public float Sensitivity;

    private static PlayerControls _instance;
    public static PlayerControls Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        Time.timeScale = 1;
    }
    
    public Vector3 GetMousePos()
    {
        if (!lockPlayer)
        {
            if (!cursorVisibility)
            {
                Vector3 vec = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                vec.z = 0f;
                return vec;
            }
        }
        var nullableVector3 = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        nullableVector3.z = 0f;
        return nullableVector3;
    }

    public bool GetMovingUp()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                return true;
            }
        }
        return false;
    }

    public float GetMovingRight()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _rv = Mathf.Clamp01(_rv * 1.33f + Sensitivity * Time.deltaTime);
                return _rv;
            }
            else
            {  
                _rv = Mathf.Clamp01(Mathf.Abs(_rv) - Sensitivity * Time.fixedDeltaTime) * Mathf.Sign(_rv);
                return _rv;
            }
        }
        return 0;
    }

    public float GetMovingLeft()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _lv = Mathf.Clamp01(_lv * 1.33f + Sensitivity * Time.deltaTime);
                return _lv;
            }
            else
            {
                _lv = Mathf.Clamp01(Mathf.Abs(_lv) - Sensitivity * Time.fixedDeltaTime) * Mathf.Sign(_lv);
                return _lv;
            }
        }
        return 0;
    }
    
    public int GetMovingRightSnap()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                return 1;
            }
        }
        return 0;
    }

    public int GetMovingLeftSnap()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                return 1;
            }
        }
        return 0;
    }
    
    
    public bool RespawnKey()
    {
        if (!respawnLock)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                return true;
            }
        }
        return false;
    }
    
    public bool GetLeftClick()
    {
        if (!lockPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                return true;
            }
        }
        return false;
    }
    
    public bool GetInteraction()
    {
        if (!lockPlayer)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                return true;
            }
        }
        return false;
    }

    public void SetLockPlayerCursorVisibility()
    {
        cursorVisibility = !cursorVisibility;
    }
}
