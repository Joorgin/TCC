using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    static List<EnemyDamage> m_List = new List<EnemyDamage>();

   public int damage;
   public PlayerMovement playerMovement;
   public static bool isAttacking;
   public static bool canTouchPlayer;

    public EnemyHeatlh thisHealth;

    public static int PercentOfDamage;

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        m_List.Add(this);
        canTouchPlayer = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Cantouch:" + canTouchPlayer);

            if (isAttacking && canTouchPlayer)
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

                if (PlayerHealth.Instance.hasShildUp == false)
                {
                    PlayerHealth.Instance.TakeDamage(damage);
                    if(PlayerHealth.hasMirrorUp)
                    {
                        int DamagePercent = (damage * (30 + PercentOfDamage)) / 100;
                        Debug.Log("DOI :" + DamagePercent);
                        thisHealth.TakeDamage(DamagePercent);
                    }
                }
                if (PlayerHealth.Instance.hasShildUp == true)
                {
                    PlayerHealth.Instance.shieldBroken = true;
                    PlayerHealth.Instance.hasShildUp = false;
                }
                isAttacking = false;
            }
        }
    }
    
}
