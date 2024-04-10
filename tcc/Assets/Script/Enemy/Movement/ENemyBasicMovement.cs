using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENemyBasicMovement : MonoBehaviour
{
    public float movementSpeed;

    public Transform PlayerTransform;
    public bool isChansing;
    public float chaseDistance;

    public bool isTrapped;
    public bool KnockFromRight;
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public Rigidbody2D rb;

    private void Awake()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (!isTrapped && PlayerHealth.isAlive)
        {
            
            if (isChansing)
            {
                if (KBCounter <= 0)
                {
                    if (transform.position.x > PlayerTransform.position.x)
                    {
                        transform.position += Vector3.left * movementSpeed * Time.deltaTime;
                        KnockFromRight = false;
                    }
                    if (transform.position.x < PlayerTransform.position.x)
                    {
                        transform.position += Vector3.right * movementSpeed * Time.deltaTime;
                        KnockFromRight = true;
                    }
                }
                else
                {
                    if (KnockFromRight == true)
                    {
                        rb.velocity = new Vector2(-KBForce,0);
                        Debug.Log(KnockFromRight);
                    }
                    if (KnockFromRight == false)
                    {
                        rb.velocity = new Vector2(KBForce,0);
                        Debug.Log(KnockFromRight);
                    }
                    KBCounter -= Time.deltaTime;
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, PlayerTransform.position) < chaseDistance)
                {
                    isChansing = true;
                }
            }
        }
        else 
        { 
            isChansing = false;
            
        }
    }
}
