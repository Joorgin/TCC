using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndaDamage : MonoBehaviour
{
    public PlayerHealth plH;
    public int damage;

    void Start()
    {
        plH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (plH.hasShildUp == true)
            {
                plH.shieldBroken = true;
                plH.hasShildUp = false;
            }
            else
            {
                plH.TakeDamage(damage);
            }
        }
    }
}
