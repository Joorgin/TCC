using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pula_Pula : MonoBehaviour
{
    Rigidbody2D player;
    public Animator anim;
    public float jumpForce;
    bool cantTouch;
    public Base_Pula_Pula basePulaPula;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!cantTouch && !basePulaPula.vemDeBaixo)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce);
            // anim.SetTrigger("Jump");
            cantTouch = true;
        }
        else if (!cantTouch && basePulaPula.vemDeBaixo)
        {
           // collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * (jumpForce / 2));
            // anim.SetTrigger("Jump");
            cantTouch = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        cantTouch = false;
    }
}
