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

    public Animator an;

    public bool alive;

    public int randomDir;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(transform.position, transform.forward * 50,Color.red);

        timer -= 1 * Time.deltaTime;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit,50))
        {     
            seePlayer = true;
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                timer = 3;
            }
        
        }  else seePlayer = false;
       

        if (timer > 0)
        {
            nav.SetDestination(player.transform.position);
            an.SetBool("running", true);
            RandomDir();
        } else
        {          
         nav.SetDestination(gameObject.transform.position);
         an.SetBool("running", false);
         transform.LookAt(new Vector3(randomDir,1,0));
         seePlayer = false;
        }

        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < 1 && seePlayer)
        {
            an.SetBool("attacking", true);
            StartCoroutine(KillPlayer());

        }
        else {
            an.SetBool("attacking", false);
            StopAllCoroutines();
        } 

        if (alive == false)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(.5f);
        Debug.Log("Player killed");
    }

    public void RandomDir()
    {
       randomDir = Random.Range(-1000,1000);
    }
}
