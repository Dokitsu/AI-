using Statestufff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class State_Terror : State<AImov>
{

    private static State_Terror _Instance;

    public static bool AIenemy;

    private State_Terror()
    {
        if (_Instance != null)
        {
            return;
        }

        _Instance = this;

    }

    public static State_Terror Instance
    {
        get
        {
            if (_Instance == null)
            {
                new State_Terror();
            }
            return _Instance;
        }
    }


    public override void EnterState(AImov _owner)
    {
        Debug.Log("Terror state enter");
        AICharacterControl.AIenemy = true;
    }

    public override void ExitState(AImov _owner)
    {
        Debug.Log("terror state exit");
    }

    public override void UpdateState(AImov _owner)
    {
        if (!_owner.teroor)
        {
            _owner.stateMachine.ChangeState(State_Default.Instance);
        }
    }
}

