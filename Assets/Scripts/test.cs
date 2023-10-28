using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 7.0f;
    public float wallJumpForce = 5.0f;
    public float wallSlideSpeed = 2.0f;

    private Rigidbody rb;
    private bool isGrounded = false;
    private bool isTouchingWall = false;
    private bool canWallJump = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);

        // Check if the player is touching a wall
        isTouchingWall = Physics.Raycast(transform.position, Vector3.right, 0.5f, LayerMask.GetMask("Wall")) ||
                         Physics.Raycast(transform.position, Vector3.left, 0.5f, LayerMask.GetMask("Wall"));

        // Move left and right
        float moveX = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveX, 0, 0) * moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Jump
        if (isGrounded)
        {
            canWallJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else if (isTouchingWall && canWallJump)
            {
                // Wall Jump
                Vector3 wallJumpDirection = isTouchingWall ? Vector3.up : Vector3.right;
                rb.AddForce(wallJumpDirection * wallJumpForce, ForceMode.Impulse);
                canWallJump = false;
            }
        }

        // Wall Slide
        if (isTouchingWall && !isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, -wallSlideSpeed, rb.velocity.z);
        }
    }
}
