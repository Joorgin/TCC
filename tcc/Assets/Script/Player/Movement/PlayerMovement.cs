using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    public float moveSpeed = 5f; 
    public Rigidbody2D rb;

    public bool isGrounded;
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
    public static bool isInFinalScene;
    public static bool isAttacking;

    public CinemachineConfiner cinemachine;

    public GameObject ConfigMenu;
    bool setactive;
    float TimeOfSlow;
    public float TimeToSetNormalSpeed;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else if(isInFinalScene)
        {
            Destroy(gameObject);
        }

        cinemachine = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineConfiner>();
        cinemachine.m_BoundingShape2D = GameObject.FindGameObjectWithTag("CameraConfiner").GetComponent<PolygonCollider2D>();

        Physics2D.IgnoreLayerCollision(6,7, true);
        Physics2D.IgnoreLayerCollision(8,7, true);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();   
    }

    void Update()
    {
        AnimatorControllers();

        rb.sharedMaterial.friction = isGrounded ?  0.24f : 0f;

        if (isInFinalScene)
        {
            Destroy(gameObject);
            isInFinalScene = false;
        }

        if (PlayerHealth.isAlive)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            if (!isAttacking)
            {
                if (isDashing)
                {
                    return;
                }

                if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)))
                {
                    Jump();
                }

                if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
                {
                    anim.SetTrigger("Dash");
                    StartCoroutine(Dash());
                }

                horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
                if (horizontalMove > 0f)
                {
                    verticalMove = 1;
                    anim.SetInteger("VerticalMove", verticalMove);
                }
                else if (horizontalMove < 0f)
                {
                    verticalMove = -1;
                    anim.SetInteger("VerticalMove", verticalMove);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setactive = !setactive;
            switch (setactive)
            {
                case true:
                    Time.timeScale = 0;
                    break;
                case false:
                    Time.timeScale = 1.5f;
                    break;
            }
            ConfigMenu.SetActive(setactive);
        }

        if(moveSpeed == 100)
        {
            TimeToSetNormalSpeed += Time.deltaTime;

            if (TimeToSetNormalSpeed >= TimeOfSlow)
            {
                moveSpeed = 300;
                TimeToSetNormalSpeed = 0;
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
                        return;
                if (!isAttacking)
                    rb.velocity = new Vector2(horizontalMove * Time.fixedDeltaTime, rb.velocity.y);
                else if(isAttacking && isGrounded)
                    rb.velocity = new Vector2(0, rb.velocity.y);
                
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

    public void StopAttack()
    {
        isAttacking = false;
    }

    void Jump()
    {
        anim.SetTrigger("IsJumping");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void AnimatorControllers()
    {
        anim.SetBool("isGrounded", isGrounded);
        

        if (isGrounded)
            anim.SetFloat("RunDirection", Input.GetAxisRaw("Horizontal"));

        if(PlayerHealth.isAlive == false && isGrounded) anim.SetBool("Dead", true);

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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Chest"))
        {
            ChestName = other.gameObject.name;
            Debug.Log("ChestName : " + ChestName);

            Chest.isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chest"))
        {
            ChestName = "";
            Chest.isInRange = false;
        }
    }

    public void TakeSlow(float TimeSlow)
    {
        Debug.Log("TAKE SLOW");
         TimeOfSlow = TimeSlow;
         moveSpeed = 100;
    }

}
