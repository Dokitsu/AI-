using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Shoot : MonoBehaviour {

    public static Shoot firerate;
    public static Shoot ddam;
    public float curfirerate;
    public float damage = 10;
    public AudioClip sfx;

    float delay = 0;
    public Transform Barrel;
    private LineRenderer Trace;

    public LayerMask enemymask;
    public LayerMask terrormask;

    public AImov enemy;


    // Use this for initialization
    void Start ()
    {
        firerate = this;
        ddam = this;

        Trace = GetComponent<LineRenderer>();
    }

    void Awake()
    {
        if (Barrel == null)
        {
            Debug.LogError("no firepoint");
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //RaycastHit hit;
        //// Does the ray intersect any objects excluding the player layer
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        //{
        //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

        //}
        //else
        //{
        //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

        //}


        if (curfirerate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shot();

            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > delay)
            {
                delay = Time.time + 1 / curfirerate;
                Shot();
            }
        }

    }

    IEnumerator RenderTrace(Vector3 hitPoint)
    {
        Trace.enabled = true;
        Trace.SetPosition(0, Barrel.position);
        Trace.SetPosition(1, Barrel.position + hitPoint);
        yield return null;
        Trace.enabled = false;
    }

    void Shot()
    {

        float shotDistance = 10000;

        Ray ray = new Ray(Barrel.position, Barrel.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shotDistance,enemymask))
        {
            if (hit.collider.GetComponent<Enemy>())
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(damage);
            }

        }

        if (Physics.Raycast(ray, out hit, shotDistance, terrormask))
        {
            enemy = hit.transform.gameObject.GetComponent<AImov>();
            enemy.shotat();

            //if (hit.collider.GetComponent<AICharacterControl>())
            //{
            //    hit.collider.GetComponent<AICharacterControl>().shotat();
            //}

        }

        StartCoroutine("RenderTrace", ray.direction * shotDistance);

        //Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);

        GetComponent<AudioSource>().Play();




    }
}
