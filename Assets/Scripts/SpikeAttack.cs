using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAttack : MonoBehaviour
{
    public GameObject bottomSpike;
    public GameObject upLeftSpike;
    public GameObject upRightSpike;
    public GameObject LeftBarSpike;
    public GameObject rightBarSpike;
    public GameObject leftBlockSpike;
    public GameObject rightBlockSpike;


    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 100;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= 1 * Time.deltaTime;

        if (timer < 100 && timer > 95)
        {
            bottomSpike.SetActive(true);
        }
        else bottomSpike.SetActive(false);

        if (timer < 95 && timer > 90)
        {
            upLeftSpike.SetActive(true);
            upRightSpike.SetActive(true);
        }
        else
        {
            upLeftSpike.SetActive(false);
            upRightSpike.SetActive(false);
        }

        if (timer < 90 && timer > 85)
        {
            LeftBarSpike.SetActive(true);
            rightBarSpike.SetActive(true);
        }
        else
        {
            LeftBarSpike.SetActive(false);
            rightBarSpike.SetActive(false);
        }

        if (timer < 85 && timer > 80)
        {
            leftBlockSpike.SetActive(true);
            rightBlockSpike.SetActive(true);
        }
        else
        {
            leftBlockSpike.SetActive(false);
            rightBlockSpike.SetActive(false);
        }

        if (timer < 80)
        {
            timer = 100;
        }
    }
}
