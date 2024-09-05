using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class ENemyBasicMovement : MonoBehaviour
{
    public float movementSpeed;

    public Transform PlayerTransform;
    public bool isChansing;
    public float chaseDistance;
    public Animator anim;

    public bool isTrapped;
    public bool KnockFromRight;
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public Rigidbody2D rb;

    public float TimeToAttack;
    bool facingRight;
    bool isgrounded;
    bool hasJumped;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.1f;

    private void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Physics2D.IgnoreLayerCollision(6, 6, true);
    }

    private void FixedUpdate()
    {
        if (TimeToAttack <= 0)
            return;
        else
            TimeToAttack -= Time.deltaTime;
    }

    private void Update()
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

            if (isChansing)
            {
                isgrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
                if (isgrounded) hasJumped = false;
                Debug.Log("GroundCheck: " + isgrounded);
                if (!isgrounded && !hasJumped)
                {
                    Jump();
                }
                if (KBCounter <= 0)
                {
                    if (Vector2.Distance(transform.position, PlayerTransform.position) < 1f)
                    {
                        Debug.Log("ATTACAR");
                        anim.SetBool("IDLE", true);
                        anim.SetBool("WALK", false);
                        CountToDamage();
                    }

                    if (transform.position.x > PlayerTransform.position.x && !EnemyDamage.isAttacking &&
                        Vector2.Distance(transform.position, PlayerTransform.position) > 1f)
                    {
                        transform.position += Vector3.left * movementSpeed * Time.deltaTime;
                        KnockFromRight = false;
                        anim.SetBool("IDLE", false);
                        anim.SetBool("WALK", true);
                    }
                    if (transform.position.x < PlayerTransform.position.x && !EnemyDamage.isAttacking &&
                        Vector2.Distance(transform.position, PlayerTransform.position) > 1f)
                    {
                        transform.position += Vector3.right * movementSpeed * Time.deltaTime;
                        KnockFromRight = true;
                        anim.SetBool("IDLE", false);
                        anim.SetBool("WALK", true);
                    }
                }
                else
                {
                    if (KnockFromRight == true)
                    {
                        rb.velocity = new Vector2(-KBForce, 0);
                        Debug.Log(KnockFromRight);
                    }
                    if (KnockFromRight == false)
                    {
                        rb.velocity = new Vector2(KBForce, 0);
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
            anim.SetBool("IDLE", true);
            anim.SetBool("WALK", false);
        }
    }

    public void CountToDamage()
    {
        if (TimeToAttack <= 0)
        {
            anim.SetTrigger("ATTACK");
            anim.SetBool("IDLE", false);
            anim.SetBool("WALK", false);
            EnemyDamage.isAttacking = true;
            TimeToAttack = 5f;
        }
        else
            return;
    }

    public void SetAttackFalse()
    {
        anim.SetBool("IDLE", true);
        EnemyDamage.isAttacking = false;
        isChansing = true;
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
