using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Void : MonoBehaviour
{
    public Transform pointToReturn;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.position = pointToReturn.position;
            PlayerHealth plyheath = collision.GetComponent<PlayerHealth>();
            plyheath.TakeDamage(damage);
        }

        if (collision.CompareTag("EnemyBoby")) Destroy(collision.gameObject);
    }
}
