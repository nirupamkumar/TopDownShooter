using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public int health = 100;
    public int maxHealth = 100;

    int IHealth.health { get => health; set => health = value; }

    int IHealth.maxHealth => maxHealth;

    public int HealthPercentage()
    {
        return (int)(((float)health / (float)maxHealth) * 100f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject, 3f);
        }
            
    }

}
