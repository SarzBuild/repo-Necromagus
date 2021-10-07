using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class BodyInteraction : Interactable
{
    public PlayerSwapManager PlayerSwapManager;

    private void OnEnable()
    {
        if (PlayerSwapManager == null)
        {
            PlayerSwapManager = PlayerSwapManager.Instance;
        }
    }

    private void GetPlayerSwapManager()
    {
       if (PlayerSwapManager == null)
       {
           PlayerSwapManager = PlayerSwapManager.Instance;
       } 
    }

    public override string GetDescription()
    {
        return "[Left Click] to change body";
    }

    public override void Interact()
    {
        ChangeBodyInit();
    }

    private void ChangeBodyInit()
    {
        PlayerSwapManager.PlayerSwap(transform.parent.gameObject);
    }
    
}
