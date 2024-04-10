using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Braceletes : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Contador_de_braceletes.instance.AlmentarBraceletes(1);
        }
    }
}
