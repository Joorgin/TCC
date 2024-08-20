using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beijo : MonoBehaviour
{
    private GameObject player;
    public float speed;
    float time;

    public PlayerHealth plH;
    public int damage;
    private void Start()
    {
        plH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        Vector3 currentScale = gameObject.transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player");
        if (PlayerMovement.verticalMove < 0)
        {
            currentScale.x = -1;
            gameObject.transform.localScale = currentScale;
        }
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        Vector3 currentScale = gameObject.transform.localScale;
        if (PlayerMovement.verticalMove > 0)
        {
            currentScale.x = 1;
            gameObject.transform.localScale = currentScale;
        }
        // Vira a flecha para a esquerda 
        if (PlayerMovement.verticalMove < 0)
        {
            currentScale.x = -1;
            gameObject.transform.localScale = currentScale;
        }

        time += Time.deltaTime;

        if (time > 5) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            plH.TakeDamage(damage);
            PlayerMovement.apaixonado = true;
            Destroy(gameObject);
        }
    }
}
