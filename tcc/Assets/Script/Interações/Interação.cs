using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interação : MonoBehaviour
{
    public GameObject button;
    public static bool buttonOff;

    private void FixedUpdate()
    {
        if(buttonOff) button.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !buttonOff)
        {
            button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !buttonOff)
        {
            button.SetActive(false);
        }
    }
}
