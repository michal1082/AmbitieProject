using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleEnemy : MonoBehaviour
{

    NavMeshAgent nav;

    public GameObject player;

    public bool seePlayer;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        seePlayer = Physics.Raycast(transform.position, transform.forward);

        Debug.DrawRay(transform.position, transform.forward * 100,Color.red);

        timer -= 1 * Time.deltaTime;
        if (seePlayer)
        {     
            timer = 3;
        }  
       

        if (timer > 0)
        {
            nav.SetDestination(player.transform.position);
        } else
        {          
         nav.SetDestination(gameObject.transform.position);           
        }
    }

    public IEnumerator LosePlayer()
    {
        yield return new WaitForSeconds(5);
        seePlayer = false;
    }
}
