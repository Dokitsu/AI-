using Statestufff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class AImov : MonoBehaviour {

    public AICharacterControl Control;

    public Vector3 targetpos;
    public Vector3 randomPoint;

    //State machine
    public bool teroor = false;
    public StateMachine<AImov> stateMachine { get; set; }

    public float Timer;
    public int second =0;

    public GameObject muzzle;
    public GameObject fov;

    public LayerMask Player;

    public EnemyManager eman;

    // Use this for initialization
    void Start ()
    {
        Control = GetComponent<AICharacterControl>();
        targetpos = this.transform.position;
        Control.SetTarget(targetpos);

        stateMachine = new StateMachine<AImov>(this);
        stateMachine.ChangeState(State_Default.Instance);
        Timer = Time.time;
    }


    void Findcover()
    {
        Setpos();
    }

    void Setpos()
    {
        Control.SetTarget(targetpos);
    }

    // Update is called once per frame
    private void Update()
    {
        //https://docs.unity3d.com/530/Documentation/ScriptReference/NavMesh.GetAreaFromName.html
        int CoverMask = 1 << NavMesh.GetAreaFromName("Cover");
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 200.0f, CoverMask))
        {
            //Debug.DrawRay(hit.position, Vector3.up, Color.blue);
            //Debug.Log(hit.position);
        }

        if(second == 1)
        {
            randomPoint = transform.position + Random.insideUnitSphere * 10;
        }
        Debug.DrawRay(randomPoint, Vector3.up, Color.blue);

        if (muzzle.gameObject.GetComponent<ShootEnemy>().cansee == false)
        {
            targetpos = randomPoint;
        }
        else
        {
            targetpos = hit.position;
        }


        Ray ray = new Ray(fov.transform.position, fov.transform.forward);
        RaycastHit hit4;
        if (Physics.Raycast(ray, out hit4, 100, Player))
        {
            if (hit4.collider.GetComponent<Player>())
            {
                if (NavMesh.SamplePosition(randomPoint, out hit, 200.0f, CoverMask))
                {
                    Debug.DrawRay(hit.position, Vector3.up, Color.blue);
                    //Debug.Log(hit.position);
                    targetpos = hit.position;
                }

            }

        }


        //if (Input.GetKeyDown("space"))
        //{
        //    print("Finding cover");
        //    Findcover();
        //}


        if (Time.time > Timer + 1)
        {
            Timer = Time.time;
            Findcover();
            second++;
        }

        if(second == 5)
        {
            teroor = false;
            Control.AIenemy = false;
            second = 0;
        }

        if (teroor == false)
        {
            if (second == 4)
            {
                stateMachine.ChangeState(State_Attack.Instance);
                second = 0;

            }
        }

        stateMachine.Update();
    }

    public void Shoot(AImov _owner)
    {
        int random = Random.Range(0, 3);
        StartCoroutine(fire(_owner, random));
    }

    public IEnumerator fire(AImov _owner, int random)
    {
        yield return new WaitForSeconds(random);
        eman = GameObject.FindWithTag("Manager").GetComponent<EnemyManager>();
        muzzle.gameObject.GetComponent<ShootEnemy>().attacking = true;
        yield return new WaitForSeconds(2);
        muzzle.gameObject.GetComponent<ShootEnemy>().attacking = false;
        _owner.stateMachine.ChangeState(State_Default.Instance);
        eman.ticketreturn();
    }


    public void shotat()
    {
        teroor = true;
        Control.AIenemy = true;
        stateMachine.ChangeState(State_Terror.Instance);
        second = 0;
        Timer = Time.time;

    }



}
