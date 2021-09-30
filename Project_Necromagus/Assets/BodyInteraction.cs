using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyInteraction : Interactable
{

    public PlayerSwapManager Body;

    private void OnEnable()
    {
        Body.SubscibeBody(transform.parent.gameObject);
    }

    private void OnDisable()
    {
        Body.UnsubscribeBody(transform.parent.gameObject);
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
        Debug.Log("BigBong");
    }
}
