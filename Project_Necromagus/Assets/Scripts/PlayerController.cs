using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControls _playerControls;

    [Header("Physics Related")] 
    public float JumpAndFallVelocity;
    public float Gravity = -9.8f;
    public float JumpForce = 15f;
    public float JumpingSpeed = 1f;
    public LayerMask GroundLayerMask;
    public float ExtraDistanceValue = 0.34f;
    private float _movingSpeed = 4;
    private Vector3 _moveTowardsPos;
    
    [Header("Sprite Leaning")]
    public Vector3 Center = Vector3.zero;
    public Vector3 Lean = new Vector3(0f, 0f, 5f);
    public Vector3 Lean2 = new Vector3(0f, 0f, 355f);

    [Header("Interaction Related")]
    public LayerMask InteractionLayerMask;
    public TMPro.TextMeshProUGUI InteractionText;
    private float _clickInteractionDistance;
    private float _holdInteractionDistance;

    [Header("Components")]
    public Collider2D Collider2D;
    private Rigidbody2D _rigidbody2D;
    private Transform _childTransform;
    private SpriteRenderer _childSpriteRenderer;
    public Animator _childAnimator;
    

    private bool _coroutineRunning;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
        _childTransform = transform.GetChild(0).GetComponent<Transform>();
        _childSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
<<<<<<< Updated upstream
        
=======
        _childAnimator = transform.GetChild(0).GetComponent<Animator>();
>>>>>>> Stashed changes
    }
    
    private void Start()
    {
        _playerControls = PlayerControls.Instance;
        _childAnimator.SetBool("IsIdle", true);
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleFall();
        DecreaseJumpSpeed();
    }

    private void Update()
    {
        CheckForInteractionObject();
    }

    private void HandleMovement()
    {
        Vector2 moveDirection = new Vector2(0, 0);
        if (_playerControls.GetMovingUp())
        {
            HandleJump();
<<<<<<< Updated upstream
        moveDirection.x = -(_playerControls.GetMovingLeft()) + (_playerControls.GetMovingRight());
        if(moveDirection.x == -1)
=======
            _childAnimator.SetBool("IsIdle", false);
            _childAnimator.SetBool("IsRunning", false);
            _childAnimator.SetBool("IsJumping", true);
        }
        if (USE_FIXED_INPUT_DEBUG)
        {
            moveDirection.x = -(_playerControls.GetMovingLeftSnap()) + (_playerControls.GetMovingRightSnap());
         
        }
        else
        {
            moveDirection.x = -(_playerControls.GetMovingLeft()) + (_playerControls.GetMovingRight());

        }
        if(Mathf.Sign(moveDirection.x) == -1f)
>>>>>>> Stashed changes
            HandleLean(true, Lean2);
        if(moveDirection.x == 1)
            HandleLean(false, Lean);
        moveDirection.Normalize();
        if (moveDirection == Vector2.zero)
        {
            _childTransform.eulerAngles = Center;
        }
        _moveTowardsPos = new Vector2(moveDirection.x * _movingSpeed * JumpingSpeed, JumpAndFallVelocity);
        _rigidbody2D.velocity = _moveTowardsPos;
        
    }

    private void HandleLean(bool flipX, Vector3 lean)
    {
        _childSpriteRenderer.flipX = flipX;
        if (Vector3.Distance(_childTransform.eulerAngles, lean) > 0.01f)
            _childTransform.eulerAngles = Vector3.Lerp(_childTransform.rotation.eulerAngles, -lean, 4f);
    }

    private IEnumerator DecreaseJumpSpeedIEnumerator()
    {
        _coroutineRunning = true;
        JumpingSpeed = 1.5f;
        yield return new WaitForSeconds(1f);
        _coroutineRunning = false;
    }
    
    private void DecreaseJumpSpeed()
    {
        if (_coroutineRunning)
            if (JumpingSpeed > 1f)
                JumpingSpeed -= Time.fixedDeltaTime*0.5f;
            else
                JumpingSpeed = 1f;
    }
    
    private void ResetJumpVariablesAndCr()
    {
        JumpAndFallVelocity = 0f;
        JumpingSpeed = 1f;
        if (_coroutineRunning)
        {
            StopCoroutine(DecreaseJumpSpeedIEnumerator());
            _coroutineRunning = false;
        }
    }

    private void HandleFall()
    {
        if (CheckCeilingCollision())
            ResetJumpVariablesAndCr();
        if (!CheckIfGrounded())
            JumpAndFallVelocity += (Gravity * _movingSpeed/2) * Time.deltaTime;
        else if (CheckIfGrounded())
        {
            _childAnimator.SetBool("IsJumping", false);
            if (JumpAndFallVelocity <= 0f)
            {
                ResetJumpVariablesAndCr();
            }
        }
    }

    private void HandleJump()
    {
        if (CheckIfGrounded())
        {
            if (JumpAndFallVelocity < 0.1)
            {
                StartCoroutine(DecreaseJumpSpeedIEnumerator());
                JumpAndFallVelocity += (_movingSpeed * JumpForce) * 0.2f;
            }
        }
    }

    private void CheckForInteractionObject()
    {
        Vector2 ray = _playerControls.GetMousePos();
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, InteractionLayerMask);
        var successfulHit = false;
        if (hit) 
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                HandleInteraction(interactable);
                InteractionText.text = interactable.GetDescription();
                var offset = new Vector2(0f,1f);
                if ((Vector2)InteractionText.transform.position != (ray + offset))
                {
                    InteractionText.transform.position = (ray + offset);
                }
                successfulHit = true;
            }
        }
        if (!successfulHit)
            InteractionText.text = "";
    }

    private void HandleInteraction(Interactable interactable)
    {
        switch (interactable.interactionType)
        {
            case Interactable.InteractionType.E:
                if (true) //OVERLAP CIRCLE RAYCAST NEAR PLAYER TO CHECK IF POSSIBILITY TO INTERACT WITH STUFF
                {
                    //IF TRUE WE SHOW THEM THAT THEY CAN INTERACT WITH IT 
                    //THEN WE LOOK IF THEY INTERACT WITH IT
                        //IF SO THEY CALL INTERACT
                }
                break;
            case Interactable.InteractionType.CLICK:
                if (_playerControls.GetLeftClick())
                {
                    interactable.Interact();
                }
                break;
        }
    }

    private RaycastHit2D CollisionCheck(float angle, Vector2 direction, float extraDistance)
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            Collider2D.bounds.center, 
            Collider2D.bounds.size / 1.5f, 
            angle, 
            direction,
            extraDistance,
            GroundLayerMask
        );
        return hit;
    }

    private bool CheckIfGrounded()
    { 
        return CollisionCheck(0f, Vector2.down, ExtraDistanceValue).collider != null;
    }

    private bool CheckCeilingCollision()
    {
        return CollisionCheck(0f, Vector2.up, ExtraDistanceValue).collider != null;
    }
}
