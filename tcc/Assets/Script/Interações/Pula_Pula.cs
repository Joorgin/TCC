using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pula_Pula : MonoBehaviour
{
    Rigidbody2D player;
    public Animator anim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 1000f);
        anim.SetTrigger("Jump");
    }
}
