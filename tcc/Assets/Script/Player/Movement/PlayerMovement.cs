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

    // bool que controla a paixao do player
    [Space]
    [Header("Tudo Sobre a paixao do player")]
    public static bool apaixonado;
    public float tempoApaixonado;
    public GameObject Sereia;
    bool hasSetSide = true;
    bool direita, esquerda;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else if (isInFinalScene)
        {
            Destroy(gameObject);
        }

        cinemachine = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineConfiner>();
        cinemachine.m_BoundingShape2D = GameObject.FindGameObjectWithTag("CameraConfiner").GetComponent<PolygonCollider2D>();

        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(8, 7, true);
        Physics2D.IgnoreLayerCollision(7, 11, true);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        AnimatorControllers();

        rb.sharedMaterial.friction = isGrounded ? 0.24f : 0f;

        if (isInFinalScene)
        {
            Destroy(gameObject);
            isInFinalScene = false;
        }

        if (apaixonado)
        {
            StartCoroutine(paixao());

            if (Sereia_Movement.RightSide && hasSetSide)
            {
                esquerda = true;
                hasSetSide = false;
            }
            // Vira a flecha para a esquerda 
            if (!Sereia_Movement.RightSide && hasSetSide)
            {
                direita = true;
                hasSetSide = false;
            }

            if (esquerda)
            {
                transform.position += Vector3.left * moveSpeed / 100 * Time.deltaTime;
                verticalMove = -1;
                anim.SetInteger("VerticalMove", verticalMove);
                anim.SetFloat("RunDirection", verticalMove);

                if (Vector2.Distance(transform.position, Sereia.transform.position) <= 3.5f) esquerda = false;
            }
            if (direita)
            {
                transform.position += Vector3.right * moveSpeed / 100 * Time.deltaTime;
                verticalMove = 1;
                anim.SetInteger("VerticalMove", verticalMove);
                anim.SetFloat("RunDirection", verticalMove);

                Sereia = GameObject.FindGameObjectWithTag("Chefe");
                if (Vector2.Distance(transform.position, Sereia.transform.position) <= 3.5f) direita = false;
            }
        }

        if (PlayerHealth.isAlive && !apaixonado)
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

        if (moveSpeed == 100)
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
                else if (isAttacking && isGrounded)
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

    IEnumerator paixao()
    {
        yield return new WaitForSeconds(tempoApaixonado);
        apaixonado = false;
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

        if (PlayerHealth.isAlive == false && isGrounded) anim.SetBool("Dead", true);

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
        if (other.gameObject.CompareTag("Chest"))
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
