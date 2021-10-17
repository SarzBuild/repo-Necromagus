using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : CollisionDetectionDependency
{
    private Collider2D _collider2D;
    private PlayerControls _playerControls;
    public float ExtraValue;

    private bool _coroutineRunning;

    private float _time;
    private float _timeSetWhenFlipped;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _playerControls = PlayerControls.Instance;
    }

    private void Update()
    {
        if (CheckForObject(_collider2D, Vector2.up, PlayerLayerMask, ExtraValue).collider != null)
        {
            if (_playerControls.PlayerTransformPoint1.position.y >= transform.position.y)
            {
                if (_playerControls.GetMovingDown())
                {
                    if (!_coroutineRunning)
                    {
                        StartCoroutine(PlayerPassThroughPlatformTimer());
                    }
                }
            }
        }
        else if (transform.position.y < _playerControls.PlayerTransformPoint1.position.y)
        {
            RemoveSelfCollision();
        }
        else
        {
            ReaddSelfCollision();
        }
    }

    private void RemoveSelfCollision()
    {
        if(!_coroutineRunning)
            _collider2D.enabled = true;
    }
    
    private void ReaddSelfCollision()
    {
        _collider2D.enabled = false;
    }

    private IEnumerator PlayerPassThroughPlatformTimer()
    {
        _coroutineRunning = true;
        _collider2D.enabled = false;
        yield return new WaitUntil(() => CheckIfPlayerPassed());
        _collider2D.enabled = true;
        _coroutineRunning = false;
    }

    private bool CheckIfPlayerPassed()
    {
        if (transform.position.y > _playerControls.PlayerTransformPoint2.position.y)
        {
            return true;
        }
        return false;
    }
}
