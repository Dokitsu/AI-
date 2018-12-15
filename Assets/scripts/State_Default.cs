using Statestufff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class State_Default : State<AImov>
{
    public AICharacterControl terrorswitch;

    private static State_Default _Instance;

    public static bool AIenemy;


    void Start()
    {

    }

    private State_Default()
    {
        if (_Instance != null)
        {
            return;
        }

        _Instance = this;

    }

    public static State_Default Instance
    {
        get
        {
            if (_Instance == null)
            {
                new State_Default();
            }
            return _Instance;
        }
    }


    public override void EnterState(AImov _owner)
    {
        Debug.Log("Default state enter");
        AICharacterControl.AIenemy = false;
    }

    public override void ExitState(AImov _owner)
    {
        Debug.Log("Defualt state exit");
    }

    public override void UpdateState(AImov _owner)
    {
        if (_owner.teroor)
        {
            _owner.stateMachine.ChangeState(State_Terror.Instance);
        }
    }
}

