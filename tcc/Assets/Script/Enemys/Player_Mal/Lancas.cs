using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lancas : MonoBehaviour
{
    public Animator anim;
    public int damage;

    private void Start()
    {
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
            if (collision.GetComponent<PlayerHealth>().hasShildUp == true)
            {
                collision.GetComponent<PlayerHealth>().shieldBroken = true;
                collision.GetComponent<PlayerHealth>().hasShildUp = false;
            }
            else
            {
                collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }
    }
}
