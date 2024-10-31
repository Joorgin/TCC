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
    public float timeToDestroy;


    void Start()
    {
        Vector3 currentScale = gameObject.transform.localScale;

        if (!Sereia_Movement.RightSide)
        {
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;
        }
        Physics2D.IgnoreLayerCollision(10, 7, true);
        Physics2D.IgnoreLayerCollision(10, 6, true);
        Physics2D.IgnoreLayerCollision(10, 11, true);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > timeToDestroy) Destroy(gameObject);

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
