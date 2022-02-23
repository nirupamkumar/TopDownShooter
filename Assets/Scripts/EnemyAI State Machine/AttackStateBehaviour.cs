using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackStateBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private Transform spawnPosition;

    public int rayDamage = 20;

    private PlayerHealth playerHealth;
    private ResetAttack attackReset;

    private EnemyController enemyController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.GetComponent<Renderer>().material.color = Color.red;

        player = GameObject.Find("Player").transform;

        spawnPosition = GameObject.Find("BarrelPoint").transform;

        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();

        attackReset = GameObject.FindObjectOfType<ResetAttack>();

        enemyController = GameObject.FindObjectOfType<EnemyController>();
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
            animator.transform.LookAt(player);

            if (!attackReset.alreadyAttacked)
            {
                RayCastAttack();

                attackReset.alreadyAttacked = true;
                attackReset.ResumeAttack();
            }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private void RayCastAttack()
    {
        
        RaycastHit hit;
        Ray ray = new Ray(spawnPosition.position, spawnPosition.forward);

        float shootRayDistance = 50f;

        if (Physics.Raycast(ray, out hit, shootRayDistance))
        {
            enemyController.Shoot();
            shootRayDistance = hit.distance;
            playerHealth.TakeDamage(rayDamage);
        }

        Debug.DrawRay(ray.origin, ray.direction * shootRayDistance, Color.red, 1);
    }

}
