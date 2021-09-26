using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerControls _playerControls;

    private int currentTimeloop;
    [SerializeField] private float speed = 4;
    private Vector3 moveTowardsPos;
    public float jumpAndFallVelocity = 1;
    private float clickInteractionDistance;
    private float holdInteractionDistance;
    public LayerMask groundLayerMask;
    public float gravity;
    public float jumpForce = 100f;
    public float jumpingSpeed = 1f;
    public bool jumping;
    private float timer;

    private Rigidbody2D rb2d;
    public Collider2D collider2d;
    private Transform childTransform;
    private SpriteRenderer childRenderer;
    
    public Vector3 center = Vector3.zero;
    public Vector3 lean = new Vector3(0f, 0f, 5f);
    public Vector3 lean2 = new Vector3(0f, 0f, 355f);
    
    public LayerMask interactionLayerMask;
    public TMPro.TextMeshProUGUI interactionText;
    
    void Start()
    {
        _playerControls = PlayerControls.Instance;

        rb2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        childTransform = transform.GetChild(0).GetComponent<Transform>();
        childRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleFall();
        CheckForInteractionObject();
    }

    void HandleMovement()
    {
        Vector3 moveDirection = new Vector3(0, 0, 0);
        if (_playerControls.GetMovingUp())
            HandleJump();
        //if (_playerControls.GetMovingDown()) 
        if (_playerControls.GetMovingLeft())
        {
            moveDirection.x = -1;
            HandleLean(true, lean2);
        }
        if (_playerControls.GetMovingRight())
        {
            moveDirection.x = +1;
            HandleLean(false, lean);
        }
        moveDirection.Normalize();
        if (moveDirection == Vector3.zero)
        {
            childTransform.eulerAngles = center;
        }
        moveTowardsPos = new Vector3(moveDirection.x * speed * jumpingSpeed, jumpAndFallVelocity, moveDirection.z * speed);
        rb2d.velocity = moveTowardsPos;
    }

    void HandleLean(bool flipX, Vector3 lean)
    {
        childRenderer.flipX = flipX;
        if (Vector3.Distance(childTransform.eulerAngles, lean) > 0.01f)
            childTransform.eulerAngles = Vector3.Lerp(childTransform.rotation.eulerAngles, -lean, 4f);
    }

    void HandleFall()
    {
        if (!CheckIfGrounded() || jumping)
            jumpAndFallVelocity += (gravity * speed/2) * Time.deltaTime;
        if (jumping)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                jumping = false;
                timer = 0f;
            }
            jumpingSpeed = 1.5f;
        }
        else if (CheckIfGrounded())
            jumpAndFallVelocity = 0f;
        else
        {
            jumpingSpeed = 1f;
        }
    }

    void HandleJump()
    {
        if (CheckIfGrounded())
        {
            if (jumpAndFallVelocity < 0.1)
            {
                jumping = true;
                jumpAndFallVelocity += (speed * jumpForce) * 0.2f;
            }
        }
    }


    void CheckForInteractionObject()
    {
        Vector2 ray = _playerControls.GetMousePos();
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, interactionLayerMask);
        var successfulHit = false;
        if (hit)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                HandleInteraction(interactable);
                interactionText.text = interactable.GetDescription();
                var offset = new Vector2(0f,1.5f);
                interactionText.transform.position = (ray + offset);
                successfulHit = true;
            }
        }
        if (!successfulHit)
            interactionText.text = "";
    }
    
    void HandleInteraction(Interactable interactable)
    {
        switch (interactable.interactionType)
        {
            case Interactable.InteractionType.HOVER:
                interactable.Interact();
                break;
            case Interactable.InteractionType.CLICK:
                if (_playerControls.GetInteraction())
                {
                    interactable.Interact();
                }
                break;
        }
    }

    bool CheckIfGrounded()
    {
        float extraDistanceValue = 0.65f;
        RaycastHit2D hit = Physics2D.BoxCast(
            collider2d.bounds.center, 
            collider2d.bounds.size / 1.5f, 
            0f, 
            Vector2.down,
            extraDistanceValue,
            groundLayerMask
            );
        return hit.collider != null;
    }

}
