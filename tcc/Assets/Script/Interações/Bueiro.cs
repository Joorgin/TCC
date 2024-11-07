using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bueiro : MonoBehaviour
{
    public GameObject bueiroObject;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Downdash._isDownDash)
            {
                bueiroObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                bueiroObject.GetComponent<BoxCollider2D>().enabled = false;
                bueiroObject.GetComponent<Rigidbody2D>().mass = 1.5f;
                StartCoroutine(Destroy());
            }
        }
    }
  //  private void OnCollisionEnter2D(Collision2D collision)
  //  {
  //      if (collision.gameObject.CompareTag("Player"))
  //      {
  //          if (Downdash._isDownDash)
  //          {
  //              gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
  //              gameObject.GetComponent<BoxCollider2D>().enabled = false;
  //              StartCoroutine(Destroy());
  //          }
  //      }
  //  }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(4f);
        Destroy(bueiroObject);
    }
}
