using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour {


    public GameObject playert;
    public GameObject muzzle;
    public GameObject look;
    private Vector3 spread;


    private void Awake()
    {
        playert = GameObject.Find("EthanNeck").gameObject;

    }

    void Update ()
    {
        spread = new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.2f, 0.2f));

        muzzle.transform.LookAt(playert.transform.position + spread);
        look.transform.LookAt((playert.transform.position) + new Vector3(0,0,0));
        transform.LookAt(playert.transform.position);
    }
}
