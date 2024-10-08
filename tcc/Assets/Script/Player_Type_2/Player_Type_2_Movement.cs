using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Type_2_Movement : MonoBehaviour
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

    public CinemachineConfiner cinemachine;

    public GameObject ConfigMenu;
    bool setactive;

    public static bool isInMainScene;

    // Tudo Sobre audio SFX
    public AudioManagert movingAudio;
    bool IsMovingComAudio = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        cinemachine = GameObject.FindGameObjectWithTag("Camera").GetComponent<CinemachineConfiner>();
        cinemachine.m_BoundingShape2D = GameObject.FindGameObjectWithTag("CameraConfiner").GetComponent<PolygonCollider2D>();
        PlayerMovement.isInFinalScene = false;
        isInMainScene = true;
        Physics2D.IgnoreLayerCollision(6, 7, true);
        GameManager.IsInMainScene = true;
        GameManager.MapsPassed = 0;
        AudioManager.hasChangedscene = true;
        AudioManager.SceneToChangeMusic = "Terreiro";
    }

    void Update()
    {
        AnimatorControllers();
        if(DialogManager.instance.isDialogActive) 
        {
            return;
        }


        rb.sharedMaterial.friction = isGrounded ? 0.24f : 0f;

     

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
            
    }

    void FixedUpdate()
    {
        if (KBCounter <= 0)
        {
            if (isDashing) return;

            if(DialogManager.instance.isDialogActive) 
            {
                Vector2 movement = new Vector2(0,0);
                rb.velocity = movement;
            }
            else
            {
                rb.velocity = new Vector2(horizontalMove * Time.fixedDeltaTime, rb.velocity.y);
                bool moving = horizontalMove != 0 ? true : false;
                if (moving && IsMovingComAudio)
                {
                    IsMovingComAudio = false;
                    movingAudio.AudioAndar();
                }
                else if (!moving || !isGrounded)
                {
                    Debug.Log("Andando com audio");
                    IsMovingComAudio = true;
                    movingAudio.AudioAndarStop();
                }
            }

            
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
        else if(DialogManager.instance.isDialogActive)
            anim.SetFloat("RunDirection", 0);

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
            Debug.Log("ChestName");

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
}
