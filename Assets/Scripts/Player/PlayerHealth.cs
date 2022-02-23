using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

interface IHealth 
{
    public int health { get; set; }
    public int maxHealth { get; }

    public GameObject gameObject { get; }
}


public class PlayerHealth : MonoBehaviour, IHealth
{
    public int health = 500;
    public int maxHealth = 500;

    int IHealth.maxHealth => maxHealth;

    int IHealth.health { get => health; set => health = value; }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Invoke(nameof(PlayerDestroy), 1.0f);
    }

    private void PlayerDestroy()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("SampleScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        int healthPackValue = 20;

        if (other.gameObject.CompareTag("HPPickup"))
        {
            Debug.Log("Gained 20 health");
            health += healthPackValue;

            Destroy(other.gameObject);
        }
    }

}
