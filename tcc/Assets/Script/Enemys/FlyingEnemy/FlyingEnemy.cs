using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    public float speed;
    private GameObject player;
    public Transform startingPoint;
    public bool chasing = false;

    public bool KnockFromRight;
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (chasing == true)
        {
            if (KBCounter <= 0)
            {
                Chase();
            }
            else
            {
                if (KnockFromRight == true)
                {
                    rb.velocity = new Vector2(-KBForce, KBForce);
                }
                if (KnockFromRight == false)
                {
                    rb.velocity = new Vector2(KBForce, KBForce);
                }
                KBCounter -= Time.deltaTime;
            }
        }
        else
        {
            ReturnToStartedPoint();
        }
        Flip();
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, player.transform.position) <= 0.5f)
        {
            // Change animation speed or something else
        }
        else
        {
            // reset health or something else
        }
    }

    public void ReturnToStartedPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
