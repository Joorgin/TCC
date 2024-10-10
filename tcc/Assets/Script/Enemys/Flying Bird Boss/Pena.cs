using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pena : MonoBehaviour
{
    public Rigidbody2D rb;
    float time;
    PlayerHealth plH;
    public int damage;

    void Start()
    {
        transform.Rotate(0, 0, Random.Range(-20f, 30f));
        rb.AddForce(transform.right * 2000);
        plH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > 2) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            plH.TakeDamage(damage);
        }
    }
}
