using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Vector2 lastCheckpointReached;
    public GameObject player;

    public void UpdateCheckpoint(Vector3 checkpointPos)
    {
        lastCheckpointReached = checkpointPos;
    }

    public void OnLoopStart()
    {
        player.transform.position = lastCheckpointReached;
    }
}
