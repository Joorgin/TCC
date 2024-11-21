using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Pula_Pula : MonoBehaviour
{
    public bool vemDeBaixo;

    private void OnTriggerExit2D(Collider2D collision)
    {
       if(collision.CompareTag("Player")) vemDeBaixo = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) vemDeBaixo = true;
    }
}
