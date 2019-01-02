using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ShootEnemy : MonoBehaviour
{

    public AICharacterControl Control;

    public static ShootEnemy firerate;
    public static ShootEnemy ddam;
    public float curfirerate;
    public float damage = 10;
    public bool attacking;

    public AudioClip sfx;

    float delay = 0;
    public Transform Barrel;
    public Transform Look;
    public LineRenderer Trace;

    public LayerMask Player;

    public AImov enemy;
    private Vector3 spread;

    public bool ty;


    // Use this for initialization
    void Start()
    {
        Control = GetComponent<AICharacterControl>();
        ty = Control.AIenemy;
        firerate = this;
        ddam = this;

        Trace = gameObject.GetComponent<LineRenderer>();
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

        if (attacking)
        {
            Ray looking = new Ray(Look.position, Look.forward);
            RaycastHit hit;
            if (Physics.Raycast(looking, out hit, 1000, Player))
            {
                if (hit.collider.GetComponent<Player>())
                {
                    if (curfirerate == 0)
                    {
                        if (ty == false)
                        {
                            Shot();
                        }
                    }
                    else
                    {
                        if (ty == false && Time.time > delay)
                        {
                            //Debug.Log("bang");
                            delay = Time.time + 1 / curfirerate;
                            Shot();
                        }
                    }
                }
            }
        }

    }

    IEnumerator RenderTrace(Vector3 hitPoint)
    {
        Trace.enabled = true;
        Trace.SetPosition(0,Barrel.position);
        Trace.SetPosition(1, (Barrel.position + hitPoint));
        yield return null;
        Trace.enabled = true;
    }

    public void Shot()
    {
        float shotDistance = 10000;

        Ray ray = new Ray(Barrel.position, Barrel.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shotDistance, Player))
        {
            if (hit.collider.GetComponent<Player>())
            {
                hit.collider.GetComponent<Player>().TakeDamage(damage);
            }

        }

        StartCoroutine("RenderTrace", ray.direction * shotDistance);

        //Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);

        GetComponent<AudioSource>().Play();

    }
}
