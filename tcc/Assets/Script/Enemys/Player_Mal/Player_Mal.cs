using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mal : MonoBehaviour
{
    // public Transform do Player
    public GameObject PlayerTransform;

    // Verifica a posi��o do Boss em Rela��o ao Player
    public static bool RightSide;
    bool facingRight;

    // Verifica o qu�o longe o player precisa estar para iniciar a persegui��o
    [Space]
    public float chaseDistance;

    // Regula a Velocidade do Boss
    [Space]
    public float movementSpeed;

    // Anima��o de ataque do Boss
    [Space]
    [Header("Anima��es e itens de ataque do Boss")]
    public Animator anim;
    public float animationTime;
    public float animationTime2;
    int RandomLanca;
    public GameObject[] lancas;
    public static bool LevantouLanca;
    public float TempoDeAtaqueDasLancas;
    bool usouAtaque1;
    public GameObject esferaDaVida;

    // Anima��o do campo de batalha
    public Animator batalhaArenaAnim;

    // Verifica o Raio de efeito do ataque 1
    [Space]
    bool playerIsInRange;
    public float bodyRadius;
    public LayerMask PlayerMask;

    // Dano que o boss Inflige
    public int damage;

    // Define o tempo entre ataques
    public float tempoEntreAtaques;
    float tempoEntreAtaquesInterno;
    bool podeAtacar = true;

    public Player_Mal_Health Player_Mal_Health;
    public Rigidbody2D rb;

    public enum States
    {
        Looking,
        Ataque1,
        Ataque2,
        Andando,
        Dead
    }

    public States states;
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player");
        batalhaArenaAnim = GameObject.FindGameObjectWithTag("Arena_De_Batalha").GetComponent<Animator>();
        lancas = GameObject.FindGameObjectsWithTag("Lanca");
        states = States.Looking;
        tempoEntreAtaquesInterno = tempoEntreAtaques;

        Physics2D.IgnoreLayerCollision(12, 11, true);
        Physics2D.IgnoreLayerCollision(12, 10, true);
        Physics2D.IgnoreLayerCollision(12, 9, true);
        Physics2D.IgnoreLayerCollision(12, 8, true);
        Physics2D.IgnoreLayerCollision(12, 7, true);
        Physics2D.IgnoreLayerCollision(12, 6, true);
    }

    void Update()
    {
        switch (states)
        {
            case States.Looking:
                Looking();
                break;
            case States.Ataque1:
                Ataque1();
                break;
            case States.Ataque2:
                Ataque2();
                break;
            case States.Andando:
                Andando();
                break;
            case States.Dead:
                Dead();
                break;
        }

        if (!podeAtacar)
        {
            tempoEntreAtaquesInterno -= Time.deltaTime;
            if (tempoEntreAtaquesInterno <= 0)
            {
                podeAtacar = true;
                tempoEntreAtaquesInterno = tempoEntreAtaques;
            }
        }

    }

    public void Looking()
    {
        if (transform.position.x > PlayerTransform.transform.position.x && facingRight)
        {
            Flip();
        }
        if (transform.position.x < PlayerTransform.transform.position.x && !facingRight)
        {
            Flip();
        }

        if (Vector2.Distance(transform.position, PlayerTransform.transform.position) < chaseDistance) states = States.Andando;

    }

    public void Andando()
    {
        if (transform.position.x > PlayerTransform.transform.position.x)
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x = 1.3f;
            gameObject.transform.localScale = currentScale;
            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
            anim.SetBool("IDLE", false);
            anim.SetBool("WALK", true);
        }else if (transform.position.x < PlayerTransform.transform.position.x)
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x = -1.3f;
            gameObject.transform.localScale = currentScale;
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
            anim.SetBool("IDLE", false);
            anim.SetBool("WALK", true);
        }else
        {
            anim.SetBool("IDLE", true);
            anim.SetBool("WALK", false);
        }

        if (Vector2.Distance(transform.position, PlayerTransform.transform.position) > 15f) states = States.Looking;
        if (Vector2.Distance(transform.position, PlayerTransform.transform.position) < 5f && podeAtacar)
        {
            if (!usouAtaque1) states = States.Ataque1;
            if (usouAtaque1) states = States.Ataque2;
        }
    }

    public void Ataque1()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        playerIsInRange = Physics2D.OverlapCircle(transform.position, bodyRadius, PlayerMask);
        StartCoroutine(Ataque11());
    }

    IEnumerator Ataque11()
    {
        usouAtaque1 = true;
        anim.SetBool("EsferaDeVida", playerIsInRange);
        rb.velocity = new Vector2(0, rb.velocity.y);
        esferaDaVida.SetActive(true);
        yield return new WaitForSeconds(animationTime);
        esferaDaVida.SetActive(false);
        anim.SetBool("EsferaDeVida", false);
        if (playerIsInRange && podeAtacar)
        {
            if (PlayerTransform.GetComponent<PlayerHealth>().hasShildUp == true)
            {
                PlayerTransform.GetComponent<PlayerHealth>().shieldBroken = true;
                PlayerTransform.GetComponent<PlayerHealth>().hasShildUp = false;
            }else
            {
                PlayerTransform.GetComponent<PlayerHealth>().TakeDamage(damage);
                Player_Mal_Health.RecuperarVida(damage);
                tempoEntreAtaquesInterno = tempoEntreAtaques;
                podeAtacar = false;
            }
        }
        states = States.Looking;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, bodyRadius);
    }

    public void Ataque2()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        StartCoroutine(Ataque22());
    }

    IEnumerator Ataque22()
    {
        yield return new WaitForSeconds(animationTime2);
        batalhaArenaAnim.SetBool("Levantar", true);
        anim.SetBool("LancasLevantar", true);
        anim.SetBool("WALK", false);
        anim.SetBool("IDLE", false);
        yield return new WaitForSeconds(3f);
        StartCoroutine(LancasParaCima());
    }

    IEnumerator LancasParaCima()
    {
        if (!LevantouLanca)
        {
            RandomLanca = Random.Range(0, lancas.Length);
            Debug.Log("Lan�a: " + RandomLanca);
            lancas[RandomLanca].GetComponent<Animator>().SetBool("Levantar", true);
            LevantouLanca = true;
        }
        yield return new WaitForSeconds(TempoDeAtaqueDasLancas);
        batalhaArenaAnim.SetBool("Levantar", false);
        anim.SetBool("LancasLevantar", false);
        anim.SetBool("IDLE", true);
        states = States.Looking;
        podeAtacar = false;
        usouAtaque1 = false;
    }

    public void Dead()
    {

    }

    // void para Virar a Sprite do Boss
    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;

        RightSide = facingRight;

        Debug.Log("Facing Right: " + facingRight);
    }
}
