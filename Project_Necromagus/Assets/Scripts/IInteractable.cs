using UnityEngine;

public abstract class IInteractable : MonoBehaviour
{
    public enum InteractionType
    {
        Click,
        Hold
    }

    private float holdTime;
    //Each script that derives from this has three different values, the type of interaction, the description and what to do when there's an interaction
    public InteractionType interactionType;
    public abstract string GetDescription();
    public abstract void Interact();

    //There's also those variables, but that's only for the hold type
    public void IncreaseHoldTime() => holdTime += Time.deltaTime;
    public void ResetHoldTime() => holdTime = 0f;
    public float GetHoldTime() => holdTime;
}
