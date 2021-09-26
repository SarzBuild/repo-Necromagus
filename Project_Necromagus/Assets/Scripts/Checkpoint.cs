using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkpointManagerObject;
    private CheckpointManager _checkpointManager;
    public LayerMask playerLayer;

    private void Start()
    {
        _checkpointManager = checkpointManagerObject.GetComponent<CheckpointManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            _checkpointManager.lastCheckpointReached = transform.position;
        }
    }
}
