using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bueiro : MonoBehaviour
{

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Downdash.HitGround)
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(Destroy());
            }
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
