using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndaDamage : MonoBehaviour
{
    public int damage;

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
