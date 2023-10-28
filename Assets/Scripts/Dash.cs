using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Dash : MonoBehaviour
{
    BoxCollider bc;
    Rigidbody rb;
    MeshRenderer mr;

    public float lookdir;
    public int direction;

    public int dashPower;

    public GameObject dashCloud;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>(); 
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lookdir = Input.GetAxisRaw("Horizontal");
        if (lookdir > 0)
        {
            direction = 1;
        } 
        if (lookdir < 0)
        {
            direction = -1;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dashing());
        }
    }

    public IEnumerator Dashing()
    {
        bc.enabled = false;
        mr.enabled = false;
        rb.useGravity = false;
        GetComponent<PlayerMovement>().canFall = false;
        rb.velocity = Vector3.zero;
        Instantiate(dashCloud, gameObject.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.4f);
        rb.velocity = Vector3.zero;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + direction * dashPower,gameObject.transform.position.y,gameObject.transform.position.z);
        yield return new WaitForSeconds(.2f);
        Instantiate(dashCloud, gameObject.transform.position, Quaternion.identity);
     
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(.1f);
        rb.velocity = Vector3.zero;
        GetComponent<PlayerMovement>().canFall = true;
        rb.useGravity = true;
        mr.enabled = true;
        bc.enabled = true;
    }
}