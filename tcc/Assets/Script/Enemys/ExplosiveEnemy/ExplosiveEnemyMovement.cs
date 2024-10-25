using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemyMovement : MonoBehaviour
{
    public enum States
    {
        Idle,
        Chasing,
        Preparing,
        Explosion
    }

    #region Public Variables 
    public States states;
    public Animator anim;
    public Transform PlayerTransform;
    public bool isTrapped;
    public float KBCounter;
    public float KBTotalTime;
    public float KBForce;
    public float movementSpeed;
    public bool KnockFromRight;
    public Rigidbody2D rb;
    public float jumpForce = 10f;
    public float chaseDistance;
    public float TimeToExplode;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.1f;
    public Transform bodyPosition;
    public float bodyRadius;
    public LayerMask PlayerMask;
    public int damage;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public float TimeOfSlow;
    #endregion

    #region Private Variables
    bool isChasing;
    bool facingRight;
    bool isgrounded;
    bool hasJumped;
    bool CanExplode;
    bool playerIsInRange;
    #endregion

    private void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        states = States.Idle;

        Physics2D.IgnoreLayerCollision(6, 6, true);
    }

    void Update()
    {
        switch (states) 
        { 
           case States.Idle:
                Idle();
                break;
           case States.Chasing:
                Chasing();
                break;
           case States.Preparing:
                Preparing();
                break;
           case States.Explosion:
                Explosion();
                break;
        }

        if(CanExplode)
        {
            TimeToExplode += Time.deltaTime;

            if(TimeToExplode >= 2 && Vector2.Distance(transform.position, PlayerTransform.position) < chaseDistance) states = States.Explosion;

            if (Vector2.Distance(transform.position, PlayerTransform.position) > chaseDistance)
            {
                CanExplode = false;
                states = States.Chasing;
            }
        }
            
    }

    public void Idle()
    {
        if (Vector2.Distance(transform.position, PlayerTransform.position) < chaseDistance)
        {
            isChasing = true;
            states = States.Chasing;
        }
    }

    public void Chasing()
    {

        if (transform.position.x > PlayerTransform.position.x && facingRight)
        {
            Flip();
        }
        if (transform.position.x < PlayerTransform.position.x && !facingRight)
        {
            Flip();
        }

        if (!isTrapped && PlayerHealth.isAlive)
        {

            if (isChasing)
            {

                isgrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
                if (isgrounded) hasJumped = false;

                if (KBCounter <= 0)
                {
                    if (Vector2.Distance(transform.position, PlayerTransform.position) <= 1.5f)
                    {
                        anim.SetBool("WALK", false);
                        states = States.Preparing;
                    }
                    if (transform.position.x > PlayerTransform.position.x &&
                        Vector2.Distance(transform.position, PlayerTransform.position) > 1.5f)
                    {
                        Debug.Log("CAO");
                        transform.position += Vector3.left * movementSpeed * Time.deltaTime;
                        KnockFromRight = false;
                        anim.SetBool("WALK", true);
                        if (!isgrounded && !hasJumped)
                        {
                            Jump();
                        }
                    }
                    if (transform.position.x < PlayerTransform.position.x &&
                        Vector2.Distance(transform.position, PlayerTransform.position) > 1.5f)
                    {
                        transform.position += Vector3.right * movementSpeed * Time.deltaTime;
                        KnockFromRight = true;
                        anim.SetBool("WALK", true);
                        if (!isgrounded && !hasJumped)
                        {
                            Jump();
                        }
                    }
                }
                else
                {
                    if (KnockFromRight == true)
                    {
                        rb.velocity = new Vector2(-KBForce, 0);
                    }
                    if (KnockFromRight == false)
                    {
                        rb.velocity = new Vector2(KBForce, 0);
                    }
                    KBCounter -= Time.deltaTime;
                }
            }
        }
    }

    public void Preparing() 
    {
        anim.SetBool("WALK", false);
        anim.SetBool("Preparing", true);
        anim.SetBool("Explosion", true);
        CanExplode = true;

        if (Vector2.Distance(transform.position, PlayerTransform.position) > chaseDistance)
        {
            isChasing = true;
            states = States.Chasing;
            CanExplode = false;
            anim.SetBool("WALK", true);
            anim.SetBool("Preparing", false);
            anim.SetBool("Explosion", false);
        }
    }

    public void Explosion() 
    {
       playerIsInRange = Physics2D.OverlapCircle(bodyPosition.position, bodyRadius, PlayerMask);

        if (playerIsInRange)
        {
            anim.SetBool("Explosion", playerIsInRange);
        }

    }

    public void DeleteCharacter()
    {
        if (playerHealth.hasShildUp == true)
        {
            playerHealth.shieldBroken = true;
            playerHealth.hasShildUp = false;
        }
        else
        {
            playerHealth.TakeDamage(damage);
            playerMovement.TakeSlow(TimeOfSlow);
            Destroy(gameObject);
        }

        
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        hasJumped = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap")) StartCoroutine(ClickTrap(3f));
    }

    public IEnumerator ClickTrap(float temp0)
    {
        isTrapped = true;
        yield return new WaitForSeconds(temp0);
        isTrapped = false;
    }
}
