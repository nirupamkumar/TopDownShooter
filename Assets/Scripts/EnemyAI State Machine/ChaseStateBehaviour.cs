using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseStateBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.GetComponent<Renderer>().material.color = Color.yellow;
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
