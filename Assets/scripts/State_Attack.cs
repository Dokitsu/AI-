using Statestufff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class State_Attack : State<AImov>
{
    private static State_Attack _Instance;
    public EnemyManager eman;
    public MonoBehaviour help;
    public bool attack;

    //void Awake()
    //{
    //    eman = GameObject.FindWithTag("Manager").GetComponent<EnemyManager>();
    //}

    private State_Attack()
    {
        if (_Instance != null)
        {
            return;
        }

        _Instance = this;
    }

    public static State_Attack Instance
    {
        get
        {
            if (_Instance == null)
            {
                new State_Attack();
            }
            return _Instance;
        }
    }


    public override void EnterState(AImov _owner)
    {
        Debug.Log("Attack state enter");
        eman = GameObject.FindWithTag("Manager").GetComponent<EnemyManager>();

        if (_owner.muzzle.gameObject.GetComponent<ShootEnemy>().cansee == true)
        {
            if (eman.canattack() == true)
            {
                _owner.Shoot(_owner);
                _owner.stateMachine.ChangeState(State_Default.Instance);
            }
            else
            {
                //eman.ticketreturn();
                _owner.stateMachine.ChangeState(State_Default.Instance);
            }
        }

    }


    public override void ExitState(AImov _owner)
    {
        //Debug.Log("Attack state exit");
        //eman.ticketreturn();
    }

    public override void UpdateState(AImov _owner)
    {
        if (_owner.teroor)
        {
            //eman.ticketreturn();
            _owner.stateMachine.ChangeState(State_Terror.Instance);
        }
    }
}

