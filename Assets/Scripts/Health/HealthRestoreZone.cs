using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestoreZone : MonoBehaviour
{
    private IHealth toHeal;

    private void OnTriggerEnter(Collider other)
    {
        var objectToHeal = other.GetComponent<IHealth>();

        if(objectToHeal != null)
        {
            toHeal = objectToHeal;
            StartCoroutine("Heal");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (toHeal == null) return;

        if(other.gameObject == toHeal.gameObject)
            StopCoroutine("Heal");
    }

    IEnumerator Heal()
    {
        for(int currentHealth = toHeal.health; currentHealth < toHeal.maxHealth; currentHealth += 1)
        {
            toHeal.health = currentHealth;
            yield return new WaitForSeconds(0.1f);
        }
        toHeal.health = toHeal.maxHealth;
    }
   
}
