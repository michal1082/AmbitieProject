using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Dash : MonoBehaviour
{
    BoxCollider bc;
    Rigidbody rb;
    MeshRenderer mr;

    public GameObject playerModel;

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
        // disbale all movement and reset velocity
        bc.enabled = false;
        playerModel.SetActive(false);
        rb.useGravity = false;
        GetComponent<PlayerMovement>().canFall = false;
        rb.velocity = Vector3.zero;
        Instantiate(dashCloud, gameObject.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.4f);

        // move player position
        rb.velocity = Vector3.zero;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + direction * dashPower,gameObject.transform.position.y,gameObject.transform.position.z);
        yield return new WaitForSeconds(.2f);

        // create cloud on new position
        Instantiate(dashCloud, gameObject.transform.position, Quaternion.identity);
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(.1f);

        // enable all movement
        rb.velocity = Vector3.zero;
        GetComponent<PlayerMovement>().canFall = true;
        rb.useGravity = true;
        playerModel.SetActive(true);
        bc.enabled = true;
    }
}
