using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pena : MonoBehaviour
{
    public Rigidbody2D rb;
    float time;
    public int damage;
    bool cantMakeDamage;

    void Start()
    {
        damage = GameManager.instance.BirdPenaDamage;
        transform.Rotate(0, 0, Random.Range(-20f, 30f));
        rb.AddForce(transform.right * 2000);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > 2) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !cantMakeDamage)
        {
            if (collision.GetComponent<PlayerHealth>().hasShildUp == true)
            {
                collision.GetComponent<PlayerHealth>().shieldBroken = true;
                collision.GetComponent<PlayerHealth>().hasShildUp = false;
                cantMakeDamage = true;
            }
            else
            {
                collision.GetComponent<PlayerHealth>().TakeDamage(damage);
                cantMakeDamage = true;
            }
        }
    }
}
