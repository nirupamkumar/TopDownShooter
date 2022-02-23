using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAttack : MonoBehaviour
{
    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    public void ResetingAttack()
    {
        alreadyAttacked = false;
    }

    public void ResumeAttack()
    {
        Invoke(nameof(ResetingAttack), timeBetweenAttacks);
    }

}
