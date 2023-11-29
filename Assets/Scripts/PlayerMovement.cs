using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;

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

    public GameObject attackPoint;

    public bool canAttack;

    public Animator an;
    // Start is called before the first frame update
    void Start()
    {
        // save start values
        horizontalSpeedStart = horizontalSpeed;
        maxVelocityXStart = maxVelocityX;

        canAttack = true;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        Animations();

        // attack
        if (Input.GetKey(KeyCode.Z) && (currentSide == 0 || grounded) && canAttack)
        {
            an.Play("Stable Sword Outward Slash");
            StartCoroutine(Attack());
        }

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

    public IEnumerator Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.transform.position,2);

        foreach(Collider enemy in hitEnemies)
        {
            // ragdoll enemy
            if (enemy.name == "Enemy")
            {
                Debug.Log(enemy.name);
            }

            if (enemy.name == "Boss")
            {
                enemy.GetComponent<Boss1>().hp -= 10;
            }

            if (enemy.name == "Boss2")
            {
                enemy.GetComponent<Boss2>().hp -= 10;
            }
        }

        canAttack = false;

        yield return new WaitForSeconds(.7f);
        canAttack = true;
    }

    public void Animations()
    {
        if (grounded)
        {
            an.SetBool("grounded", true);
        } else an.SetBool("grounded", false);

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            an.SetBool("isRunning", true);
        } else an.SetBool("isRunning", false);

        if (currentSide != 0 && !grounded)
        {
            an.SetBool("onWall", true);
        }
        else an.SetBool("onWall", false);
      
    }

    void OnCollisionEnter(Collision collision)
    {
            if (collision.gameObject.CompareTag("Gem1"))
            {
                SceneManager.LoadScene(0);
                GameManager.Instance.hasRedGem = true;
            }

        if (collision.gameObject.CompareTag("Gem2"))
        {
            SceneManager.LoadScene(0);
            GameManager.Instance.hasBlueGem = true;
        }

        if (collision.gameObject.CompareTag("Boss1Teleport"))
        {
            SceneManager.LoadScene(2);
            GameManager.Instance.hasBlueGem = true;
        }

        if (collision.gameObject.CompareTag("Boss2Teleport"))
        {
            SceneManager.LoadScene(4);
            GameManager.Instance.hasBlueGem = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("teleport1"))
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("teleport");
                SceneManager.LoadScene(1);
            }
        }

        if (other.gameObject.CompareTag("teleport2"))
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                SceneManager.LoadScene(3);
            }
        }

        if (other.gameObject.CompareTag("teleportMiddle"))
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                SceneManager.LoadScene(5);
            }
        }
    }
}
