using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ShootEnemy : MonoBehaviour
{

    public static ShootEnemy firerate;
    public static ShootEnemy ddam;
    public float curfirerate;
    public float damage = 10;
    public AudioClip sfx;

    float delay = 0;
    public Transform Barrel;
    private LineRenderer Trace;

    public LayerMask enemymask;

    public AImov enemy;

    public static bool AIenemy;


    // Use this for initialization
    void Start()
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
    void Update()
    {

        //if (curfirerate == 0)
        //{
        //    if (Input.GetButtonDown("Fire1"))
        //    {
        //        Shot();

        //    }
        //}
        //else
        //{
        //    if (Input.GetButton("Fire1") && Time.time > delay)
        //    {
        //        delay = Time.time + 1 / curfirerate;
        //        Shot();
        //    }
        //}


        if (curfirerate == 0)
        {
            if (AICharacterControl.AIenemy == false)
            {
                Shot();
            }
        }
        else
        {
            if (AICharacterControl.AIenemy == false && Time.time > delay)
            {
                Debug.Log("bang");
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

    public void Shot()
    {
        Debug.Log("bang");
        float shotDistance = 10000;

        Ray ray = new Ray(Barrel.position, Barrel.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shotDistance, enemymask))
        {
            if (hit.collider.GetComponent<Enemy>())
            {
                hit.collider.GetComponent<Enemy>().TakeDamage(damage);
            }

        }


        StartCoroutine("RenderTrace", ray.direction * shotDistance);

        //Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);

        GetComponent<AudioSource>().Play();




    }
}
