using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolingStateBehaviour : StateMachineBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask isGround;
    
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;

        //animator.GetComponent<Renderer>().material.color = Color.yellow;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!walkPointSet)
        {
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(animator.transform.position.x + randomX, animator.transform.position.y, animator.transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -animator.transform.up, 2f, isGround))
                walkPointSet = true;
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = animator.transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state


    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

}
