using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilador : MonoBehaviour
{
    public Rigidbody2D playerRigidBody;

    void Start()
    {
        playerRigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !PlayerMovement.isDashing)
        {
            playerRigidBody.AddForce(transform.up * 900);
            //playerRigidBody.useAutoMass = true;
        }
    }
}
