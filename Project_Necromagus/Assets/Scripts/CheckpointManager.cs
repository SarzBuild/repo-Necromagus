using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Vector2 lastCheckpointReached;
    public GameObject player;

    private void Start()
    {
        
    }

    public void UpdateCheckpoint(Vector3 checkpointPos)
    {
        lastCheckpointReached = checkpointPos;
    }

    private void OnLoopStart()
    {
        player.transform.position = lastCheckpointReached;
    }
}
