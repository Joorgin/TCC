using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderGroundMenager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")) gameObject.transform.position += new Vector3(0, 10, 0);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")) gameObject.transform.position += new Vector3(0, 10, 0);
    }
}
