using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkpointManagerObject;
    private CheckpointManager _checkpointManager;
    public LayerMask playerLayer;
    public Timer TimerReset;
    private int _tmpTime;

    private void Start()
    {
        _checkpointManager = checkpointManagerObject.GetComponent<CheckpointManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            _checkpointManager.UpdateCheckpoint(transform.position);
            _tmpTime = (int)TimerReset.timer;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            TimerReset.timer = _tmpTime;
        }
    }
}
