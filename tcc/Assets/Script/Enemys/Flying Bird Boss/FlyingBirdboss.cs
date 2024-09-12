using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class FlyingBirdboss : MonoBehaviour
{
    private GameObject player;
    bool facingRight = true;
    bool TimeToAttack;
    bool TimeToStopAppear;
    public float timeToAttack;

    public float speed;
    public Rigidbody2D rb;
    // public Animator anim;
    public float dashPower;
    public float dashingTime;
    float TimeToApear;
    public float flipThresholdAngle = 90f;
    public GameObject Pena;
    public Transform ThrowPoint;

    private bool canDash = true; // Whether the object can dash

    public PlayerHealth playerHealth;

    bool isAttacking;
    bool canMakeDamage = true;
    bool MakeDash;
    public Animator anim;
    float Side = 1;

    public enum States
    {
        Appering,
        Looking,
        Attack1,
        Attack2,
        Dead
    }

    public States states;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        states = States.Looking;
    }

    private void Update()
    {
        switch (states)
        {
            case States.Appering:
                Aperring();
                break;
            case States.Looking:
                Looking();
                break;
            case States.Attack1:
                Attaking1();
                break;
            case States.Attack2:
                Attaking2();
                break;
            case States.Dead:
                Dead();
                break;
        }


        // Tempo pra atacar
        if (TimeToAttack)
        {

            timeToAttack += Time.deltaTime;

            if (timeToAttack >= 8) Debug.Log("CHANGE ANIMATION");

            if (timeToAttack >= 10)
            {
                if (!MakeDash)
                {
                    states = States.Attack1;
                    MakeDash = true;
                }
                else if (MakeDash)
                {
                    states = States.Attack2;
                    MakeDash = false;
                }
                TimeToAttack = false;
                timeToAttack = 0;
            }
        }

        if (TimeToStopAppear)
        {
            TimeToApear += Time.deltaTime;

            if (TimeToApear >= 1)
            {
                EndOfAnimationApear();
                TimeToStopAppear = false;
            }
        }

    }

    public void Aperring()
    {
        // anim.SetBool("Appear", true);
        TimeToStopAppear = true;
    }

    public void EndOfAnimationApear()
    {
        states = States.Looking;
    }

    public void Looking()
    {
        Vector3 vectorToTarget = player.transform.position - transform.position;

        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(player.transform.position.y - gameObject.transform.position.y, player.transform.position.x - gameObject.transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

        if (transform.position.x > player.transform.position.x && facingRight)
        {
            Flip();
        }
        if (transform.position.x < player.transform.position.x && !facingRight)
        {
            Flip();
        }
        TimeToAttack = true;
    }

    public void Attaking1()
    {
        StartCoroutine(Dash(Side));
    }

    public void Attaking2()
    {
        anim.SetTrigger("Pena");
        for (int i = 0; i < 4; i++)
        {
            GameObject temp = Instantiate(Pena, ThrowPoint.position, ThrowPoint.transform.rotation);
        }
        states = States.Looking;
    }

    public void Dead()
    {

    }

    private IEnumerator Dash(float direction)
    {
        anim.SetBool("Rasante", true);
        canDash = false;
        isAttacking = true;
        rb.velocity = new Vector2(transform.localScale.x * (dashPower * Side), 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.velocity = new Vector2(0, 0);
        StartCoroutine(Dash2());
        
    }

    IEnumerator Dash2()
    {
        rb.velocity = new Vector2(transform.localScale.x * (dashPower * Side), 0f);
        yield return new WaitForSeconds(dashingTime);
        canDash = true;
        isAttacking = false;
        canMakeDamage = true;
        states = States.Looking;
        anim.SetBool("Rasante", false);
        yield return new WaitForSeconds(dashingTime + 1);
        rb.velocity = new Vector2(0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isAttacking && canMakeDamage)
            {
                playerHealth.TakeDamage(20);
                canMakeDamage = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isAttacking && canMakeDamage)
            {
                playerHealth.TakeDamage(20);
                canMakeDamage = false;
            }
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;

        if (transform.position.x > player.transform.position.x && facingRight)
        {
            currentScale.y = -1;
            Side = -1;
            gameObject.transform.localScale = currentScale;
        }
        else
        {
            currentScale.y = 1;
            Side = 1;
            gameObject.transform.localScale = currentScale;
        }

        facingRight = !facingRight;
    }

}
