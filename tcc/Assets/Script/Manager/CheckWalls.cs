using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWalls : MonoBehaviour
{
    public static string Name;
    public static bool isInWall;
    public float Radius;
    public LayerMask groundLayer;

    private void Start()
    {
    }

    private void Update()
    {
        isInWall = Physics2D.OverlapCircle(gameObject.transform.position, Radius, groundLayer);

        if (isInWall) Name = gameObject.name;
        else Name = null;
    }
}
