using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;

public class GroundCollision : MonoBehaviour
{
    public float jumpAndFallVelocity;
    private Vector3 moveTowardsPos;
    private Rigidbody2D rb2d;
    public float gravity = -9.8f;
    public float speed = 4f;
    public Collider2D collider2d;
    public LayerMask groundLayerMask;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleFall();
    }
    
    void HandleMovement()
    {
        moveTowardsPos = new Vector3(0, jumpAndFallVelocity, 0);
        rb2d.velocity = moveTowardsPos;
    }

    void HandleFall()
    {
        if (!CheckIfGrounded())
            jumpAndFallVelocity += (gravity * speed/2) * Time.deltaTime;
        else if (CheckIfGrounded())
            jumpAndFallVelocity = 0f;
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
            groundLayerMask);
            return hit.collider != null;
    }
}
