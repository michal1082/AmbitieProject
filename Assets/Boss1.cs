using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public float hp = 100;
    public GameObject gem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < 1)
        {
            gem.SetActive(true);
            Destroy(gameObject);
        }
    }
}
