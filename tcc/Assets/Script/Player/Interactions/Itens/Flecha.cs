using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public Rigidbody2D rb;
    float time;
    EnemyHeatlh enemyHealth;
    ExplosiveEnemyHealth explosiveEnemyHealth;

    void Start()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        // Vira a flecha para a direita
        if (PlayerMovement.verticalMove > 0)
        {
            rb.AddForce(transform.right * 2000);
        }
        // Vira a flecha para a esquerda 
        if (PlayerMovement.verticalMove < 0)
        {
            currentScale.x = -1;
            gameObject.transform.localScale = currentScale;
            rb.AddForce(transform.right * -2000);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 2) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBoby"))
        {
            Debug.Log(gameObject.name);
        }
    }
}
