using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Grapples : MonoBehaviour
{
    bool grappleOne;
    bool grappleTwo;
    bool grappleThree;
    bool grappleFour;

    public GameObject grapple;
    public float rotateSpeed;

    public GameObject lrGo1;
    public GameObject lrGo2;
    public GameObject lrGo3;
    public GameObject lrGo4;

    LineRenderer lr1;
    LineRenderer lr2;
    LineRenderer lr3;
    LineRenderer lr4;

    // Start is called before the first frame update
    void Start()
    {
        lr1 = lrGo1.GetComponent<LineRenderer>();
        lr2 = lrGo2.GetComponent<LineRenderer>();
        lr3 = lrGo3.GetComponent<LineRenderer>();
        lr4 = lrGo4.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        grapple.transform.Rotate(0,0,1 * rotateSpeed * Time.deltaTime);

     
     


        /*   grappleOne = Physics.Raycast(transform.position, Vector3.up, float.PositiveInfinity);
           grappleTwo = Physics.Raycast(transform.position, Vector3.down, float.PositiveInfinity);
           grappleThree = Physics.Raycast(transform.position, Vector3.right, float.PositiveInfinity);
           grappleFour = Physics.Raycast(transform.position, Vector3.left, float.PositiveInfinity);*/

        Debug.DrawRay(grapple.transform.position, grapple.transform.up * 20, Color.yellow);
        Debug.DrawRay(grapple.transform.position, grapple.transform.right * 20, Color.yellow);
        Debug.DrawRay(grapple.transform.position, -grapple.transform.right * 20, Color.yellow);
        Debug.DrawRay(grapple.transform.position, -grapple.transform.up * 20, Color.yellow);


        RaycastHit hitUp;
        if (Physics.Raycast(grapple.transform.position, grapple.transform.up, out hitUp, 20))
        {
            Debug.DrawLine(grapple.transform.position, hitUp.transform.position);
            lr1.enabled = true;
            lr1.SetPosition(0, transform.position);
            lr1.SetPosition(1, hitUp.transform.position);
        }
        else lr1.enabled = false;

        RaycastHit hitDown;
        if (Physics.Raycast(grapple.transform.position, -grapple.transform.up, out hitDown, 20))
        {
            Debug.DrawLine(transform.position, hitDown.transform.position);
            lr2.enabled = true;
            lr2.SetPosition(0, transform.position);
            lr2.SetPosition(1, hitDown.transform.position);
        }
        else lr2.enabled = false;

        RaycastHit hitLeft;
        if (Physics.Raycast(grapple.transform.position, -grapple.transform.right, out hitLeft, 20))
        {
            Debug.DrawLine(transform.position, hitLeft.transform.position);
            lr3.enabled = true;
            lr3.SetPosition(0, transform.position);
            lr3.SetPosition(1, hitLeft.transform.position);
        }
        else lr3.enabled = false;

        RaycastHit hitRight;
        if (Physics.Raycast(grapple.transform.position, grapple.transform.right, out hitRight, 20))
        {
            Debug.DrawLine(transform.position, hitRight.transform.position);
            lr4.enabled = true;
            lr4.SetPosition(0, transform.position);
            lr4.SetPosition(1, hitRight.transform.position);
        }
        else lr4.enabled = false;

    }
}
