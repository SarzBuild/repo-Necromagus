using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateEvent : CollisionDetectionDependency
{
    public GameEvent PressurePlateDown;
    public GameEvent PressurePlateUp;

    public Sprite PressurePlateUpSprite;
    public Sprite PressurePlateDownSprite;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    public float RayMaxDist;
    
    private bool _coroutineRunning;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        StandingOnPressurePlate();
    }

    private void StandingOnPressurePlate()
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
        PressurePlateDown.Raise();
        _spriteRenderer.sprite = PressurePlateDownSprite;
        yield return new WaitWhile(() => DetectedSomething());
        _spriteRenderer.sprite = PressurePlateUpSprite;
        PressurePlateUp.Raise();
        _coroutineRunning = false;
        StopCoroutine(CheckCoroutine());
    }

    private bool DetectedSomething()
    {
        if (CheckForObject(_collider2D, PlayerLayerMask, RayMaxDist).collider != null ||
            CheckForObject(_collider2D, DeadBodyLayerMask, RayMaxDist).collider != null)
            return true;
        else
            return false;
    }
}
