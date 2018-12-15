using System;
using System.Collections;
using UnityEditorInternal;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Vector3 target;                                    // target to aim for

        public GameObject Raggy;

        public static bool AIenemy;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;

            AIenemy = this;
            AIenemy = false;
        }


        private void Update()
        {
            if (target != null)
                agent.SetDestination(target);

            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity, false, false);
            //else
            //    character.Move(Vector3.zero, true, false);

            if (AIenemy == true)
            {
                character.Move(Vector3.zero, true, false);
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        }


        public void SetTarget(Vector3 target)
        {
            this.target = target;
        }

        public void Ragdeath()
        {
            GameObject rag = Instantiate(Raggy, transform.root.transform.position, Quaternion.identity) as GameObject;
        }


    }

}
