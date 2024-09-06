using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    static List<EnemyDamage> m_List = new List<EnemyDamage>();

   public int damage;
   public PlayerHealth playerHealth;
   public PlayerMovement playerMovement;
   public static bool isAttacking;

    public EnemyHeatlh thisHealth;

    public static int PercentOfDamage;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        m_List.Add(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isAttacking)
            {
                playerMovement.KBCounter = playerMovement.KBTotalTime;

                if (collision.transform.position.x <= transform.position.x)
                {
                    playerMovement.KnockFromRight = true;
                }
                if (collision.transform.position.x > transform.position.x)
                {
                    playerMovement.KnockFromRight = false;
                }

                if (PlayerHealth.hasShildUp == false)
                {
                    playerHealth.TakeDamage(damage);
                    if(PlayerHealth.hasMirrorUp)
                    {
                        int DamagePercent = (damage * (30 + PercentOfDamage)) / 100;
                        Debug.Log("DOI :" + DamagePercent);
                        thisHealth.TakeDamage(DamagePercent);
                    }
                }
                if (PlayerHealth.hasShildUp == true)
                {
                    PlayerHealth.shieldBroken = true;
                    PlayerHealth.hasShildUp = false;
                }
                isAttacking = false;
            }
        }
    }
    
}
