using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_UnderGround : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position += new Vector3(0, 4, 0);
        }
    }
}
