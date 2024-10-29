using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    static List<EnemyDamage> m_List = new List<EnemyDamage>();

   public int damage;
   public PlayerMovement playerMovement;
   public static bool isAttacking;

    public EnemyHeatlh thisHealth;

    public static int PercentOfDamage;

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        m_List.Add(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isAttacking)
            {
                Debug.Log("Sereia Attack");
                playerMovement.KBCounter = playerMovement.KBTotalTime;

                if (collision.transform.position.x <= transform.position.x)
                {
                    playerMovement.KnockFromRight = true;
                }
                if (collision.transform.position.x > transform.position.x)
                {
                    playerMovement.KnockFromRight = false;
                }

                if (collision.GetComponent<PlayerHealth>().hasShildUp == false)
                {
                    collision.GetComponent<PlayerHealth>().TakeDamage(damage);
                    if(PlayerHealth.hasMirrorUp)
                    {
                        int DamagePercent = (damage * (30 + PercentOfDamage)) / 100;
                        Debug.Log("DOI :" + DamagePercent);
                        thisHealth.TakeDamage(DamagePercent);
                    }
                }
                if (collision.GetComponent<PlayerHealth>().hasShildUp == true)
                {
                    collision.GetComponent<PlayerHealth>().shieldBroken = true;
                    collision.GetComponent<PlayerHealth>().hasShildUp = false;
                }
                isAttacking = false;
            }
        }
    }
    
}
