using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour {


    public Transform player;
    public GameObject muzzle;
    private Vector3 spread;


    // Update is called once per frame
    void Update ()
    {
        spread = new Vector3(Random.Range(-0.6f, 0.6f), Random.Range(-0.2f, 0.2f) + 1f);

        muzzle.transform.LookAt(player.position + spread);
        transform.LookAt(player.position);
    }
}
