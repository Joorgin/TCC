using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onda : MonoBehaviour
{
    public Rigidbody2D rb;
    float time;
    public float movementSpeed;
    bool hasSetSide = true;
    bool direita, esquerda;

    void Start()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        // Vira a flecha para a direita
        if (PlayerMovement.verticalMove > 0)
        {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }
        // Vira a flecha para a esquerda 
        if (PlayerMovement.verticalMove < 0)
        {
            currentScale.x = -1;
            gameObject.transform.localScale = currentScale;
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        }

        Physics2D.IgnoreLayerCollision(10, 7, true);
        Physics2D.IgnoreLayerCollision(10, 6, true);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 2) Destroy(gameObject);

        if (Sereia_Movement.RightSide && hasSetSide)
        {
            direita = true;
            hasSetSide = false;
        }
        // Vira a flecha para a esquerda 
        if (!Sereia_Movement.RightSide && hasSetSide)
        {
            esquerda = true;
            hasSetSide = false;
        }

        if (esquerda) transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        if (direita) transform.position += Vector3.right * movementSpeed * Time.deltaTime;
    }

}
