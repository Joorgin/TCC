using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Void : MonoBehaviour
{
    public Transform pointToReturn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.position = pointToReturn.position;
            PlayerHealth plyheath = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            plyheath.TakeDamage(10);
        }
    }
}
