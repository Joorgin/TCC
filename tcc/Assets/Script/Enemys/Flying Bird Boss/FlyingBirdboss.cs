using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float dashDistance = 5f; // The distance the object will dash
    public float dashDuration = 0.2f; // The duration of the dash
    public float dashCooldown = 1f; // The cooldown between dashes
    private bool canDash = true; // Whether the object can dash

    public PlayerHealth playerHealth;

    bool isAttacking;
    bool canMakeDamage = true;
    bool MakeDash;
    public Animator anim;

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
        states = States.Appering;
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
        if(TimeToAttack)
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

        if(TimeToStopAppear)
        {
            TimeToApear += Time.deltaTime;

            if(TimeToApear >= 1)
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
        Vector3 directionToTarget = player.transform.position - transform.position;

        // Rotate the object to look at the target
        transform.right = directionToTarget.normalized;

        ThrowPoint.transform.right = directionToTarget.normalized;

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
      StartCoroutine(Dash());
    }

    public void Attaking2() 
    {
        anim.SetTrigger("Pena");
        for (int i = 0; i < 4; i++)
        {
            GameObject temp = Instantiate(Pena, ThrowPoint.position,ThrowPoint.transform.rotation);
        }
        states = States.Looking;
    }

    public void Dead() 
    { 
    
    }

    private IEnumerator Dash()
    {
        anim.SetBool("Rasante", true);
        canDash = false;
        isAttacking = true;

        // Get the direction the object is facing
        Vector2 dashDirection = transform.right;

        // Calculate the dash destination
        Vector2 dashDestination = (Vector2)transform.position + dashDirection * dashDistance;

        float elapsedTime = 0f;
        Vector2 initialPosition = transform.position;

        // Smoothly move towards the dash destination over dashDuration
        while (elapsedTime < dashDuration)
        {
            transform.position = Vector2.Lerp(initialPosition, dashDestination, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches exactly to the dash destination
        transform.position = dashDestination;

        yield return new WaitForSeconds(dashingTime);

        Vector2 dashDirection2 = transform.right;

        // Calculate the dash destination
        Vector2 dashDestination2 = (Vector2)transform.position + dashDirection2 * (dashDistance * 2);

        Debug.Log("DASHING BIRD");

        float elapsedTime2 = 0f;
        Vector2 initialPosition2 = transform.position;

        // Smoothly move towards the dash destination over dashDuration
        while (elapsedTime2 < dashDuration)
        {
            transform.position = Vector2.Lerp(initialPosition2, dashDestination2, elapsedTime2 / dashDuration);
            elapsedTime2 += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches exactly to the dash destination
        transform.position = dashDestination2;

        yield return new WaitForSeconds(dashingTime);

        canDash = true;
        isAttacking = false;
        canMakeDamage = true;
        states = States.Looking;
        anim.SetBool("Rasante", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
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
        if(collision.CompareTag("Player"))
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
            gameObject.transform.localScale = currentScale;
        }
        else
        {
            currentScale.y = 1;
            gameObject.transform.localScale = currentScale;
        }

       facingRight = !facingRight;
    }

}