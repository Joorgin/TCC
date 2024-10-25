using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lancas : MonoBehaviour
{
    public Animator anim;
    public PlayerHealth playerHealth;
    public int damage;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void AbaixarLancas()
    {
        anim.SetBool("Levantar", false);
        Player_Mal.LevantouLanca = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (playerHealth.hasShildUp == true)
            {
                playerHealth.shieldBroken = true;
                playerHealth.hasShildUp = false;
            }
            else
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
