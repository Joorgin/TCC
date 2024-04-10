using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public Rigidbody2D rb;

    private bool isGrounded;
    private float horizontalMove = 0f;

    public static int verticalMove;

    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.1f;

    private bool canDash = true;
    public static bool isDashing;
    private float dashPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;

    public bool KnockFromRight;
    public Animator anim;
    private Vector3 upTowards;
    private Vector3 EndTowards;

    public static string ChestName;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();   
    }

    void Update()
    {
        AnimatorControllers();
        if (PlayerHealth.isAlive)
        {

            if (isDashing)
            {
                return;
            }

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                Debug.Log("dashing");
                StartCoroutine(Dash());
            }

            horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
            if (horizontalMove > 0f)
            {
                verticalMove = 1;
                anim.SetInteger("VerticalMove", verticalMove);
            }
            else if(horizontalMove < 0f)
            {
                verticalMove = -1;
                anim.SetInteger("VerticalMove", verticalMove);
            }
        }
    }

    void FixedUpdate()
    {
        if (PlayerHealth.isAlive)
        {
            if (KBCounter <= 0)
            {
                if (isDashing)
                {
                    return;
                }

                Vector2 movement = new Vector2(horizontalMove * Time.fixedDeltaTime, rb.velocity.y);
                rb.velocity = movement;
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

    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void AnimatorControllers()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("IsJumping", isGrounded && Input.GetKeyDown(KeyCode.Space));

        if (isGrounded)
            anim.SetFloat("RunDirection", Input.GetAxisRaw("Horizontal"));

    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(verticalMove * dashPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Chest"))
        {
            ChestName = collision.gameObject.name;
            Chest.isInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chest"))
        {
            ChestName = "";
            Chest.isInRange = false;
        }
    }

}
