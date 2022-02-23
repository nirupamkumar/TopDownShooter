using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform spawnPosition;
    public int projectileDamage = 30;

    //pooling system
    private PoolingSystem bulletPool;
    [SerializeField] private GameObject bulletPrefab;

    private void Start()
    {
        bulletPool = new PoolingSystem(bulletPrefab);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootRayCast();
        }
    }

    void ShootRayCast()
    {
        RaycastHit hit;
        Ray ray = new Ray(spawnPosition.position, spawnPosition.forward);

        float shootRayDistance = 25f;

        if(Physics.Raycast(ray, out hit, shootRayDistance))
        {
            ShootBullet();
            var enemy = hit.collider.gameObject.GetComponent<EnemyHealth>();
            shootRayDistance = hit.distance;
            enemy?.TakeDamage(projectileDamage);
        }

        Debug.DrawRay(ray.origin, ray.direction * shootRayDistance, Color.red, 1);
    }

    
    void ShootBullet()
    {
        var bullet = bulletPool.GetObject(spawnPosition.position, spawnPosition.rotation);
        bullet.GetComponent<Bullet>().SetDirection(spawnPosition.forward);
    }

}

    


