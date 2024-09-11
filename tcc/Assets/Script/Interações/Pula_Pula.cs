using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pula_Pula : MonoBehaviour
{
    Rigidbody2D player;
    public Animator anim;
    public float jumpForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce);
        anim.SetTrigger("Jump");
    }
}
