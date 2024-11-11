using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWalls : MonoBehaviour
{
    public static CheckWalls instance { get; private set; }
    public string Name;
    public bool isInWall;
    public float Radius;
    public LayerMask groundLayer;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        isInWall = Physics2D.OverlapCircle(gameObject.transform.position, Radius, groundLayer);

        if (isInWall) Name = gameObject.name;
        else Name = null;
    }
}
