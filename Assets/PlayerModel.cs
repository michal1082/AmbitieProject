using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public float turnTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, 90, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * turnTime);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, -90, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * turnTime);
        }
    }
}
