using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public float Damage;
    Collider2D[] enemiesToDamage;
    public Transform attackPos;
    public float attackRange;
    public LayerMask WhatIsEnemies;


    private void FixedUpdate()
    {
        Debug.Log("DownDash");
        Vector3 ataquePosicao = attackPos.position;

        enemiesToDamage = Physics2D.OverlapCircleAll(ataquePosicao, attackRange, WhatIsEnemies);
        foreach (Collider2D etd in enemiesToDamage)
        {
            //basic Enemy
            if (etd.GetComponent<ENemyBasicMovement>() != null)
            {
                etd.GetComponent<EnemyHeatlh>().TakeDamage(Damage);
            }

            //Explosive Enemy
            if (etd.GetComponent<ExplosiveEnemyMovement>() != null)
            {
                etd.GetComponent<ExplosiveEnemyHealth>().TakeDamage(Damage);
            }

        }
    }
   
}
