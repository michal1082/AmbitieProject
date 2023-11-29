using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss2 : MonoBehaviour
{
    public float hp = 300;
    public GameObject player;
    public float rotationSpeed = 5.0f;
    public float movementSpeed = 2.0f;
    public float chargingMovementSpeed = 12.0f; 

    public bool isCharging;
    public bool stunned;

    private float chargeCooldown = 6f;
    private float chargeTimer = 0f;
    private float stunDuration = 5f;
    private float stunTimer = 0f;
    private Vector3 chargeDirection;

    public Animator an;

    public GameObject gem1;

    void Update()
    {
        if (hp < 1)
        {
            an.SetBool("isStunned", true);
            stunned = true;
            gameObject.GetComponent<Boss2>().enabled = false;
            gem1.SetActive(true);
        }

        if (!stunned)
        {
            an.SetBool("isStunned", false);
            LookAtPlayer();
            if (!isCharging)
            {
                an.SetBool("isRunning", false);
                MoveTowardsPlayer();
            }

            chargeTimer += Time.deltaTime;
            if (chargeTimer >= chargeCooldown)
            {
                ChargeAtPlayer();
                chargeTimer = 0f;
                an.SetBool("isRunning", true);
            }
        }
        else
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunDuration)
            {
                stunned = false;
                stunTimer = 0f;
            }
        }
    }

    public void LookAtPlayer()
    {
        if (!isCharging)
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            newRotation.x = transform.rotation.x;
            newRotation.z = transform.rotation.z;
            transform.rotation = newRotation;
        }
    }

    public void MoveTowardsPlayer()
    {
        Vector3 directionToMove = player.transform.position - transform.position;
        directionToMove.y = 0f; 
        directionToMove.z = 0f; 

        directionToMove.Normalize();
        Vector3 newPosition = transform.position + (directionToMove * movementSpeed * Time.deltaTime);
        newPosition.y = 2.97f;
        transform.position = newPosition;
    }

    public void ChargeAtPlayer()
    {
        if (!isCharging)
        {
            chargeDirection = transform.forward; 
            isCharging = true;
            StartCoroutine(StopCharge());
        }
    }

    IEnumerator StopCharge()
    {
        yield return new WaitForSeconds(3f); 

        isCharging = false;
        an.SetBool("isRunning", false);
    }

    void FixedUpdate()
    {
        if (isCharging)
        {
            Vector3 newPosition = transform.position + (chargeDirection * chargingMovementSpeed * Time.fixedDeltaTime);
            newPosition.y = 2.97f; 
            newPosition.z = 0f;
            transform.position = newPosition;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (isCharging)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                Stun();
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }
    }

    public void Stun()
    {
        stunned = true;
        isCharging = false;
        an.SetBool("isStunned", true);
    }
}
