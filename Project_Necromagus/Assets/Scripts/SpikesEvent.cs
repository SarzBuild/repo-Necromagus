using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesEvent : CollisionDetectionDependency
{
    public GameEvent PlayerTouchedSpikes;
    
    private Collider2D _collider2D;
    public float RayMaxDist;
    
    private bool _coroutineRunning;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        PlayerInSpikes();
    }

    public void PlayerInSpikes()
    {
        if (DetectedSomething())
        {
            if (!_coroutineRunning)
            {
                StartCoroutine(CheckCoroutine());
            }
        }
    }
    
    private IEnumerator CheckCoroutine()
    {
        _coroutineRunning = true;
        PlayerTouchedSpikes.Raise();
        yield return new WaitWhile(() => DetectedSomething());
        _coroutineRunning = false;
        StopCoroutine(CheckCoroutine());
    }
    
    private bool DetectedSomething()
    {
        if (CheckForObject(_collider2D, PlayerLayerMask, RayMaxDist).collider != null)
            return true;
        else
            return false;
    }
}
