using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pula_Pula : MonoBehaviour
{
    Rigidbody2D player;
    public Animator anim;
    public float jumpForce;
    bool cantTouch;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!cantTouch)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce);
            Debug.Log("LAUNCH");
            // anim.SetTrigger("Jump");
            cantTouch = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        cantTouch = false;
    }
}
