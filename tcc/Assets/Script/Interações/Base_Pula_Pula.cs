using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Pula_Pula : MonoBehaviour
{
    public bool vemDeBaixo;

    private void OnTriggerExit2D(Collider2D collision)
    {
        vemDeBaixo = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        vemDeBaixo = true;
    }
}
