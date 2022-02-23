using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIBehaviour : MonoBehaviour
{
    #region Variables
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public int projecileDamage = 10;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Transform spawnPosition;

    private PlayerHealth playerHealth;
    #endregion

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();

        if (playerInAttackRange && playerInSightRange)
        { 
            AttackPlayer();
            
        }

    }


    #region Enemy Patroling Logic
    private void Patroling()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    #endregion


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }


    #region Attack Player Logic
    private void AttackPlayer()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;

        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {

            //Attack player approach - raycast or projectile
            AttackRayCast();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void AttackRayCast()
    {
        RaycastHit hit;
        Ray ray = new Ray(spawnPosition.position, -spawnPosition.up);

        float shootRayDistance = 50f;

        if (Physics.Raycast(ray, out hit, shootRayDistance))
        {
            shootRayDistance = hit.distance;
            playerHealth.TakeDamage(projecileDamage);
        }

        Debug.DrawRay(ray.origin, ray.direction * shootRayDistance, Color.red, 1);
    }

    #endregion


    #region Sphere Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sightRange);
    }
    #endregion


}
