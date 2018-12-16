using Statestufff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class State_Attack : State<AImov>
{
    private static State_Attack _Instance;

    public static bool AIenemy;


    void Start()
    {

    }

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
        AICharacterControl.AIenemy = false;
    }

    public override void ExitState(AImov _owner)
    {
        Debug.Log("Attack state exit");
    }

    public override void UpdateState(AImov _owner)
    {
        if (_owner.teroor)
        {
            _owner.stateMachine.ChangeState(State_Terror.Instance);
        }
    }
}

