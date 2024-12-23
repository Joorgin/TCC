using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    public static bool isGrounded;
    public float horizontalMove = 0f;

    public static int verticalMove;

    public float jumpForce = 10f;
    bool hasDoubleJump;
    bool jumping = false;
    public float JumpMaxTime = 1;
    float jumpTime = 1;
    public Transform groundCheck;
    public LayerMask groundLayer, groundLayer2;
    public float groundCheckRadius = 0.1f;

    // Tudo sobre o dash do Player
    private bool canDash = true;
    public static bool isDashing;
    private float dashPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 5f;

    [Space]
    [Header("Animator das UI Habilidades")]
    public Animator animDash;
    public static float CoolDownAnimationMultiplier = 1;

    // define tudo sobre knockback que o player sofre
    [Space]
    [Header("Tudo sobre o KnockBack que o Player sofre")]
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;

    public Animator anim;
    private Vector3 upTowards;
    private Vector3 EndTowards;

    // Define qual bau o player esta interagindo.... para n�o abrir todos de uma vez
    public static string ChestName;

    // Aqui define se o player sera Destruido para n�o ir na mainScene
    public static bool isInFinalScene;

    // Define se a movimentacao do player deve ou n�o parar para atacar
    public static bool isAttacking;

    

    // Item upgrade inteacoes
    public static string IntemName;

    // Sim o config do menu esta aqui, n�o me pergunte o pq....
    [Space]
    [Header("Config do Menu")]
    public GameObject ConfigMenu;
    public bool setactive;

    // Configura o slow que o inimigo explosivo ou qualquer outro inimigo que aplique o mesmo efeito
    float TimeOfSlow;
    public float TimeToSetNormalSpeed;

    // bool que controla a paixao do player
    [Space]
    [Header("Tudo Sobre a paixao do player")]
    public static bool apaixonado;
    public float tempoApaixonado;
    public GameObject Sereia;
    bool hasSetSide;
    bool direita, esquerda;

    // Define qual a chance do bau bom
    public static int chanceForAGoodChest;

    private void Awake()
    {

        if (Instance == null) Instance = this;
        else if (isInFinalScene) Destroy(gameObject);

        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(8, 7, true);
        Physics2D.IgnoreLayerCollision(7, 14, true);
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

        if (apaixonado)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            Sereia = GameObject.FindGameObjectWithTag("Chefe");
            StartCoroutine(paixao());

            if (Sereia_Movement.RightSide && !hasSetSide)
            {
                esquerda = true;
                hasSetSide = true;
            }
            if (!Sereia_Movement.RightSide && !hasSetSide)
            {
                direita = true;
                hasSetSide = true;
            }

            if (esquerda)
            {
                transform.position += Vector3.left * moveSpeed / 100 * Time.deltaTime;
                verticalMove = -1;
                anim.SetInteger("VerticalMove", verticalMove);
                anim.SetFloat("RunDirection", verticalMove);
                anim.SetBool("Apaixonado", true);
                if (Vector2.Distance(transform.position, Sereia.transform.position) <= 3.5f)
                {
                    esquerda = false;
                    anim.SetBool("Apaixonado", true);
                }
            }
            if (direita)
            {
                transform.position += Vector3.right * moveSpeed / 100 * Time.deltaTime;
                verticalMove = 1;
                anim.SetInteger("VerticalMove", verticalMove);
                anim.SetFloat("RunDirection", verticalMove);
                anim.SetBool("Apaixonado", true);

                if (Vector2.Distance(transform.position, Sereia.transform.position) <= 3.5f)
                {
                    direita = false;
                    anim.SetBool("Apaixonado", true);
                }
            }
        }else anim.SetBool("Apaixonado", false);

        if (PlayerHealth.Instance.isAlive && !apaixonado && !PlayerAttack.isShooting && !GameManager.instance.isInConversation && !setactive)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            if (Downdash._isDownDash && isGrounded)
            {
                rb.velocity = Vector3.zero;
                Downdash.CanDownDash = true;
                Downdash._isDownDash = false;
                PlayerHealth.shackCamera = true;
                Downdash.Damage();
            }

            if (isDashing) return;

            //if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))) Jump();
            if (isGrounded && !jumping)
            {
                jumpTime = JumpMaxTime;
                hasDoubleJump = false;
                jumpForce = 7f;
            }

            if(!isGrounded && !jumping && !hasDoubleJump && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumpTime = JumpMaxTime;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                hasDoubleJump = true;
                jumping = true;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)){
                
                jumping = true;
                anim.SetTrigger("IsJumping");
            }

            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow))
            {
               jumping = false;
            }

           
           // if (!isGrounded && !hasDoubleJump && (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))) { Jump(); hasDoubleJump = true; }

           // if (isGrounded) hasDoubleJump = false;

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
        if(GameManager.instance.isInConversation) rb.velocity = new Vector2(0, rb.velocity.y);

        if (PlayerHealth.Instance.isAlive && !apaixonado && !Downdash._isDownDash && !PlayerAttack.isShooting && !GameManager.instance.isInConversation && !setactive)
        {
            if (KBCounter <= 0)
            {
                if (isDashing) return;

                rb.velocity = new Vector2(horizontalMove * Time.fixedDeltaTime, rb.velocity.y);
                bool moving = horizontalMove != 0 ? true : false;
            }
            else
            {
                if (KnockFromRight == true) rb.velocity = new Vector2(-KBForce, KBForce);
                if (KnockFromRight == false) rb.velocity = new Vector2(KBForce, KBForce);
                KBCounter -= Time.deltaTime;
            }
        }
        if (jumping)
        {
            jumpTime -= Time.fixedDeltaTime;
            jumpTime = Mathf.Clamp(jumpTime, 0, JumpMaxTime);
          
            Jump(jumpTime);
        }

    }

    IEnumerator paixao()
    {
        yield return new WaitForSeconds(tempoApaixonado);
        apaixonado = false;
        hasSetSide = false;
    }

    public void StopAttack()
    {
        isAttacking = false;
    }

    void Jump(float force)
    {
        rb.AddForce(new Vector2(0, jumpForce * force),ForceMode2D.Impulse);
    }

    public void AnimatorControllers()
    {
        anim.SetBool("isGrounded", isGrounded);
        if (isGrounded && !apaixonado && !GameManager.instance.isInConversation) anim.SetFloat("RunDirection", Input.GetAxisRaw("Horizontal"));
        if (PlayerHealth.Instance.isAlive == false && isGrounded) anim.SetBool("Dead", true);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(verticalMove * dashPower, 0f);
        animDash.SetFloat("SpeedAnimation", CoolDownAnimationMultiplier);
        animDash.SetBool("Dashou", true);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        animDash.SetBool("Dashou", false);
        canDash = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Chest"))
        {
            ChestName = other.gameObject.name;
            Chest.isInRange = true;
        }

        if (other.gameObject.CompareTag("ItemUpgrade"))
        {
            IntemName = other.gameObject.name;
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
        TimeOfSlow = TimeSlow;
        moveSpeed = 100;
    }

    public static void AddChanceOfAChest()
    {
        chanceForAGoodChest++;

        if (chanceForAGoodChest >= 4) return;

        Debug.Log("RARITYCHANCE: " + PlayerMovement.chanceForAGoodChest);
    }


    

}
