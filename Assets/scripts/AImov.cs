using Statestufff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class AImov : MonoBehaviour {

    public AICharacterControl Control;

    public Vector3 targetpos;

    //State machine
    public bool teroor = false;
    public StateMachine<AImov> stateMachine { get; set; }

    public float Timer;
    public int second =0;

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
        // https://docs.unity3d.com/530/Documentation/ScriptReference/NavMesh.GetAreaFromName.html
        int CoverMask = 1 << NavMesh.GetAreaFromName("Cover");
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 200.0f, CoverMask))
        {
            Debug.DrawRay(hit.position, Vector3.up, Color.blue);
            //Debug.Log(hit.position);
        }
        targetpos = hit.position;

        if (Input.GetKeyDown("space"))
        {
            print("Finding cover");
            Findcover();
        }


        if(Time.time > Timer + 1)
        {
            Timer = Time.time;
            second++;
        }

        if(second == 5)
        {
            second = 0;
            teroor = false;
        }
        stateMachine.Update();
    }


    public void shotat()
    {
        teroor = true;
        stateMachine.ChangeState(State_Terror.Instance);
        second = 0;
        Timer = Time.time;
    }



}
