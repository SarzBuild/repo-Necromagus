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
    public bool jumping;
    private float timer;

    private Rigidbody2D rb2d;
    public Collider2D collider2d;
    private Transform childTransform;
    private SpriteRenderer childRenderer;
    
    public Vector3 center = Vector3.zero;
    public Vector3 lean = new Vector3(0f, 0f, 5f);
    public Vector3 lean2 = new Vector3(0f, 0f, 355f);
    

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
        //HandleLean();
    }

    void HandleMovement()
    {
        //Then we make it so that the player movement related inputs are put inside a Vector3
        Vector3 moveDirection = new Vector3(0, 0, 0);
        if (_playerControls.GetMovingUp())
            HandleJump();
        if (_playerControls.GetMovingDown()) 
            moveDirection.y = -1;
        if (_playerControls.GetMovingLeft())
        {
            moveDirection.x = -1;
            childRenderer.flipX = false;
            if (childRenderer.flipX == false)
            {
                if (Vector3.Distance(childTransform.eulerAngles, lean2) > 0.01f)
                    childTransform.eulerAngles = Vector3.Lerp(childTransform.rotation.eulerAngles, -lean2, 4f);
            }
        }
        if (_playerControls.GetMovingRight())
        {
            moveDirection.x = +1;
            childRenderer.flipX = true;
            if (childRenderer.flipX)
            {
                if (Vector3.Distance(childTransform.eulerAngles, lean) > 0.01f)
                    childTransform.eulerAngles = Vector3.Lerp(childTransform.rotation.eulerAngles, -lean, 4f);
            }
        }
        moveDirection.Normalize(); //We normalize the values
        if (moveDirection == Vector3.zero)
        {
            childTransform.eulerAngles = center;
        }
        //We create a new vector3 that'll be our main moving variable and we set the velocity accordingly
        moveTowardsPos = new Vector3(moveDirection.x, jumpAndFallVelocity, moveDirection.z);
        //We set the velocity of the player accordingly
        rb2d.velocity = moveTowardsPos * speed;
    }

    void HandleFall()
    {
        if (!CheckIfGrounded() || jumping)
            //If we are falling, we gradually add more velocity to mimic the acceleration when falling
            jumpAndFallVelocity += gravity * Time.deltaTime;
        if (jumping)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                jumping = false;
                timer = 0f;
            }

        }
        
        else if (CheckIfGrounded())
            //If we are grounded, we reset the velocity to zero
            jumpAndFallVelocity = 0f;
    }

    void HandleJump()
    {
        if (CheckIfGrounded())
        {
            jumping = true;
            jumpAndFallVelocity += (speed * jumpForce) * 0.2f;
        }
    }

    bool CheckIfGrounded()
    {
        //This hard-coded float is to set a maximum length to the cast
        float extraDistanceValue = 0.65f;
        RaycastHit2D hit = Physics2D.BoxCast(
            collider2d.bounds.center, 
            collider2d.bounds.size / 1.5f, 
            0f, 
            Vector2.down,
            extraDistanceValue,
            groundLayerMask
            ); //We cast a ray underneath the player to see if they collide with something that is the chosen Layer Mask.
        return hit.collider != null;
    }

}
