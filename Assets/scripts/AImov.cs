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
    public GameObject point;

    public LayerMask Player;

    public EnemyManager eman;
    public Transform playert;

    public float distance;


    private void Awake()
    {
        point = Instantiate(fov, transform.position, transform.rotation);
        playert = GameObject.FindWithTag("Player").gameObject.transform;
        targetpos = new Vector3(0, 0f, 0);
    }
    void Start ()
    {
        Control = GetComponent<AICharacterControl>();
        //targetpos = this.transform.position;
        Control.SetTarget(targetpos);

        stateMachine = new StateMachine<AImov>(this);
        stateMachine.ChangeState(State_Default.Instance);
        Timer = Time.time;
    }


    void Findcover()
    {
        //https://docs.unity3d.com/530/Documentation/ScriptReference/NavMesh.GetAreaFromName.html
        int CoverMask = 1 << NavMesh.GetAreaFromName("Cover");
        NavMeshHit hit;
        //if (NavMesh.SamplePosition(transform.position, out hit, 200.0f, CoverMask))
        //{
        //    //Debug.DrawRay(hit.position, Vector3.up, Color.blue);
        //    //Debug.Log(hit.position);
        //}

        //if (second == 1)
        //{
        //    randomPoint = transform.position + Random.insideUnitSphere * 10;
        //}
        //Debug.DrawRay(randomPoint, Vector3.up, Color.blue);

        //if (muzzle.gameObject.GetComponent<ShootEnemy>().cansee == false)
        //{
        //    targetpos = randomPoint;
        //}
        //else
        //{
        //    targetpos = hit.position;
        //}

        if(point == null)
        {
        }

        randomPoint = transform.position + Random.insideUnitSphere * 100;

        Ray ray = new Ray(point.transform.position, point.transform.forward);
        Debug.DrawRay(point.transform.position, point.transform.forward, Color.blue);
        RaycastHit hit4;
        distance = Vector3.Distance(playert.transform.position, point.transform.position);

        if (Physics.Raycast(ray, out hit4, 1000, Player))
        {
            if (hit4.collider.GetComponent<Player>())
            {
                //NavMeshHit hit;
                Debug.Log("HII");
                if (NavMesh.SamplePosition(randomPoint, out hit, 500.0f, CoverMask))
                {
                    Debug.DrawRay(hit.position, Vector3.up, Color.blue);
                    targetpos = hit.position;

                    //Findcover();
                }
            }
        }

        //if (NavMesh.SamplePosition(randomPoint, out hit, 500.0f, CoverMask) && Vector3.Distance(playert.transform.position, transform.position) > 10)
        //{
        //    Debug.DrawRay(hit.position, Vector3.up, Color.blue);
        //    targetpos = hit.position;
        //    //Findcover();
        //}




        //targetpos = hit.position;
        Setpos();
    }

    void Setpos()
    {
        Control.SetTarget(targetpos);
    }

    // Update is called once per frame
    private void Update()
    {
        point.transform.LookAt((playert.transform.position) + new Vector3(0, 0.5f, 0));
        point.transform.position = targetpos + new Vector3(0, 0.5f, 0);

        //if (Input.GetKeyDown("space"))
        //{
        //    print("Finding cover");
        //    Findcover();
        //}

        if (Time.time > Timer + 1)
        {
            Timer = Time.time;
            Findcover();
            Setpos();
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
