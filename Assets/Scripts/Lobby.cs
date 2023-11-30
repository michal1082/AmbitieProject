using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{

    public GameObject blueGem;
    public GameObject redGem;

    public GameObject portal;

    // Start is called before the first frame update
    void Start()
    {
        blueGem.SetActive(false); 
        redGem.SetActive(false); 
        portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.hasBlueGem == true)
        {
            blueGem.SetActive(true);
        }
        if (GameManager.Instance.hasRedGem == true)
        {
            redGem.SetActive(true);
        }

        if (GameManager.Instance.hasRedGem == true && GameManager.Instance.hasBlueGem == true)
        {
            portal.SetActive(true);
        }
    }
}
