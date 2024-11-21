using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndaDamage : MonoBehaviour
{
    public int damage;
    bool canDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !canDamage)
        {
            if (PlayerHealth.Instance.hasShildUp == true)
            {
                PlayerHealth.Instance.shieldBroken = true;
                PlayerHealth.Instance.hasShildUp = false;
                canDamage = true;
            }
            else
            {
                PlayerHealth.Instance.TakeDamage(damage);
                canDamage = true;
            }
        }
    }
}
