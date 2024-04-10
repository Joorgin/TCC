using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    static List<EnemyDamage> m_List = new List<EnemyDamage>();

   public int damage;
   public PlayerHealth playerHealth;
   public PlayerMovement playerMovement;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        m_List.Add(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
            }
            if(PlayerHealth.hasShildUp == true)
            {
               PlayerHealth.hasShildUp = false;
            }
        }
    }
    
}
