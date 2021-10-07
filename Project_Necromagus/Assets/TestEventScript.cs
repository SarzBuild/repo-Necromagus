using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestEventScript : MonoBehaviour
{
    public LayerMask PlayerLayerMask;
    public UnityEvent EnterTriggerEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((PlayerLayerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            EnterTriggerEvent.Invoke();
        }
    }
}
