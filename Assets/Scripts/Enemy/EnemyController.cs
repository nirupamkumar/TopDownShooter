using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    private Transform target;

    [SerializeField] private EnemyHealth enemyHealth;

    //BulletPooling
    private PoolingSystem enemyBulletPool;
    [SerializeField] private GameObject bulletPrefab;
    public Transform spawnPosition;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        enemyBulletPool = new PoolingSystem(bulletPrefab);
    }

    void FixedUpdate()
    {
        var distance = Vector3.Distance(transform.position, target.position);

        animator.SetFloat("distanceFromPlayer", distance);
        animator.SetInteger("healthPercentage", enemyHealth.HealthPercentage());

        Debug.Log(enemyHealth.HealthPercentage());
    }

    public void Shoot()
    {
        var enemyBullet = enemyBulletPool.GetObject(spawnPosition.position, spawnPosition.rotation);
    }
}
