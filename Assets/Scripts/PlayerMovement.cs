using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    float horizontal;
    float vertical;

    public float horizontalSpeed;
    public float horizontalAirSpeed;
    public float verticalSpeed;

    public float jumpForce;
    public float wallJumpForce;
    public float wallJumpForceUp;

    float extraForce = 500;


    public float maxVelocityX;
    public float maxVelocityXAir;
    public bool jumping;
    public bool canJump;
    public bool canFall;
    public bool canMove;
    public bool isFalling;

    public float additionalGravity; 
    public float deceleration;

    public bool grounded;
    public bool rightSide;
    public bool leftSide;

    public int currentSide;

    public float turnTime;

    private float horizontalSpeedStart;
    private float maxVelocityXStart;

    public float highscore;

    public Animator an;
    // Start is called before the first frame update
    void Start()
    {
        // save start values
        horizontalSpeedStart = horizontalSpeed;
        maxVelocityXStart = maxVelocityX;

        rb = GetComponent<Rigidbody>();
        highscore = 0;
    }

    // Update is called once per frame
    void Update()
    {

        Animations();

       if (Input.GetKey(KeyCode.R))
        {
            highscore = 0;
        }
        
        if (transform.position.y > highscore)
        {
            highscore = transform.position.y;
        }
        Debug.Log(highscore);

        horizontal = horizontalSpeed * Input.GetAxisRaw("Horizontal");
        vertical = verticalSpeed * Input.GetAxisRaw("Vertical");

        grounded = Physics.Raycast(transform.position, -Vector3.up, 0.5f + 0.03f, 3);

        leftSide = Physics.Raycast(transform.position, -Vector3.right, 0.5f + 0.03f);
        rightSide = Physics.Raycast(transform.position, -Vector3.left, 0.5f + 0.03f);

        isFalling = rb.velocity.y < 0;
        jumping = Input.GetKeyDown(KeyCode.Space);

      

        // move horizontal
        if (canMove)
        {
            rb.AddForce(new Vector3(horizontal * extraForce, 0, 0) * Time.deltaTime, ForceMode.Force);
        }

        if (jumping)
        {
            if (grounded)
            {
                rb.velocity = new Vector3(rb.velocity.x,0,0);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        if (grounded)
        {
            // set air speed
            horizontalSpeed = horizontalSpeedStart;

            // max ground velocity
            maxVelocityX = maxVelocityXStart;

        }

        if (!grounded)
        {
            // set air speed
            horizontalSpeed = horizontalAirSpeed;

            // max air velocity
            maxVelocityX = maxVelocityXAir;

            // wall jump
            if (currentSide != 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(TimeOutMove());
                    rb.velocity = new Vector3(0, 0, 0);
                    rb.AddForce(new Vector3(currentSide * wallJumpForce, wallJumpForceUp, 0), ForceMode.Impulse);
                }
                rb.drag = 5;
            }
            else rb.drag = 0f;
        }

        // clamp velocity      
        Vector3 clampedVelocity = rb.velocity;
        clampedVelocity.x = Mathf.Clamp(rb.velocity.x, -maxVelocityX, maxVelocityX);
        rb.velocity = clampedVelocity;

        // check current side
        if (leftSide == true)
        {
            currentSide = 1;
        }
        else if (rightSide == true) { currentSide = -1; } else currentSide = 0;
    }

    private void FixedUpdate()
    {
        if (canFall)
        {
            if (!grounded && !isFalling && !Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(Vector3.down * additionalGravity * 1.2f * Time.deltaTime, ForceMode.Acceleration);
            }
            // Apply additional downward force
            if (isFalling)
            {
                rb.AddForce(Vector3.down * additionalGravity * Time.deltaTime, ForceMode.Acceleration);
            }
        }
     
        // drag
        if (horizontal == 0 && grounded)
        {
            Vector3 decelerationForce = -rb.velocity * deceleration;
            rb.AddForce(decelerationForce * Time.deltaTime, ForceMode.Force);
            if (rb.velocity.magnitude < 0.1)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    public IEnumerator TimeOutMove()
    {
        canMove = false;
        yield return new WaitForSeconds(.1f);
        canMove = true;
    }

    public void Animations()
    {
        if (grounded)
        {
            an.SetBool("grounded", true);
        } else an.SetBool("grounded", false);

        if (Input.GetAxis("Horizontal") != 0)
        {
            an.SetBool("isRunning", true);
        } else an.SetBool("isRunning", false);
    }
}
