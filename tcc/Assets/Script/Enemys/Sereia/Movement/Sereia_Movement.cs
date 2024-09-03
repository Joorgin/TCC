using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sereia_Movement : MonoBehaviour
{
    // Localizacao do player
    public Transform PlayerTransform;

    // Verifica a posicao da sereia e sua movimentacao
    bool facingRight;
    public float movementSpeed;
    public float chaseDistance;

    // Fala a direcao que a onda deve ir
    public static bool RightSide;

    // Animacao na sereia
    [Space]
    [Header("Informacoes da animacao da sereia")]
    public Animator anim;
    public float ondaAnimationTime;
    public float paixaoAnimationTime;
    public float cajadadaAnimationTime;


    // Tudo sobre a onda
    [Space]
    [Header("Primeiro Ataque (Onda)")]
    public GameObject onda;
    public GameObject ondaStartLocation;

    // Tempo para o proximo ataque e variavel que identifica se pode atacar
    [Space]
    [Header("Tempo Entre Ataques")]
    public float timeToAttack;
    public bool canAttack = true;
    bool hasAttacked1;

    // Beijo
    [Space]
    [Header("Beijo")]
    public GameObject saidaDoBeijo;
    public GameObject Beijo;

    public enum States
    {
        Andando,
        Idle,
        Attack1,
        Attack2,
        Attack3,
        Dead
    }

    // Estados em que a sereia pode estar
    [Space]
    public States state;

    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        state = States.Idle;
        Physics2D.IgnoreLayerCollision(12, 11, true);
        Physics2D.IgnoreLayerCollision(12, 10, true);
        Physics2D.IgnoreLayerCollision(12, 9, true);
        Physics2D.IgnoreLayerCollision(12, 8, true);
        Physics2D.IgnoreLayerCollision(12, 7, true);
        Physics2D.IgnoreLayerCollision(12, 6, true);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case States.Idle:
                Idle();
                break;
            case States.Andando:
                Andando();
                break;
            case States.Attack1:
                Attack1();
                break;
            case States.Attack2:
                Attack2();
                break;
            case States.Attack3:
                Attack3();
                break;
            case States.Dead:
                Dead();
                break;
        }
    }

    void Idle()
    {
        if (transform.position.x > PlayerTransform.position.x && facingRight)
        {
            Flip();
        }
        if (transform.position.x < PlayerTransform.position.x && !facingRight)
        {
            Flip();
        }

        if (!canAttack)
        {
            timeToAttack -= Time.deltaTime;
            if (timeToAttack <= 0)
            {
                canAttack = true;
                timeToAttack = 10f;
            }
        }

        if (Vector2.Distance(transform.position, PlayerTransform.position) > chaseDistance)
        {
            state = States.Andando;
        }
        if (Vector2.Distance(transform.position, PlayerTransform.position) <= chaseDistance && canAttack)
        {
            if (hasAttacked1) state = States.Attack2;
            else state = States.Attack1;
        }
    }

    void Andando()
    {
        Debug.Log(state);

        if (transform.position.x > PlayerTransform.position.x && facingRight)
        {
            Flip();
        }
        if (transform.position.x < PlayerTransform.position.x && !facingRight)
        {
            Flip();
        }

        if (!canAttack)
        {
            timeToAttack -= Time.deltaTime;
            if (timeToAttack <= 0)
            {
                canAttack = true;
                timeToAttack = 10f;
            }
        }

        if (Vector2.Distance(transform.position, PlayerTransform.position) <= 5.5f && canAttack)
        {
            if (hasAttacked1) state = States.Attack2;
            else state = States.Attack1;
        }
    }

    public void Attack1()
    {
        StartCoroutine(CriarOnda());
    }

    IEnumerator CriarOnda()
    {
        Debug.Log("ATTACK1");
        //anim.SetBool("Attack", true);
        yield return new WaitForSeconds(ondaAnimationTime);
        if (canAttack) Instantiate(onda, ondaStartLocation.transform.position, Quaternion.identity);
        canAttack = false;
        hasAttacked1 = true;
        state = States.Idle;
    }

    void Attack2()
    {
        StartCoroutine(ApaixonarPlayer());
    }

    IEnumerator ApaixonarPlayer()
    {
        //anim.SetBool("Paixao", true);
        yield return new WaitForSeconds(paixaoAnimationTime);
        if (canAttack) Instantiate(Beijo, saidaDoBeijo.transform.position, Quaternion.identity);
        canAttack = false;
        yield return new WaitForSeconds(3f);
        if (Vector2.Distance(transform.position, PlayerTransform.position) <= 3.5f) state = States.Attack3;
        else
        {
            state = States.Idle;
            hasAttacked1 = false;
        }
    }

    void Attack3()
    {
        StartCoroutine(Cajadada());
    }

    IEnumerator Cajadada()
    {
        //anim.SetBool("Cajadada", true);
        Debug.Log("ATTACK3");
        yield return new WaitForSeconds(cajadadaAnimationTime);
        state = States.Idle;
        hasAttacked1 = false;
    }

    void Dead()
    {
        Debug.Log("DEAD");
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;

        Debug.Log("Facing Right: " + facingRight);
    }
}
