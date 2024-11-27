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
            if (PlayerHealth.Instance.hasShildUp == true)
            {
                PlayerHealth.Instance.shieldBroken = true;
                PlayerHealth.Instance.hasShildUp = false;
            }
            else
            {
                PlayerHealth.Instance.TakeDamage(damage);
            }
        }
    }
}
