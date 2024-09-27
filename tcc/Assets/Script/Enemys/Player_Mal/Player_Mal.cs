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

    // Anima��o do Boss
    [Space]
    public Animator anim;
    public float animationTime;

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
        states = States.Looking;
        tempoEntreAtaquesInterno = tempoEntreAtaques;
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

        if(!podeAtacar)
        {
            tempoEntreAtaquesInterno -= Time.deltaTime;
            if(tempoEntreAtaquesInterno <= 0)
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
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
           // anim.SetBool("IDLE", false);
           // anim.SetBool("WALK", true);
        }
        if (transform.position.x < PlayerTransform.transform.position.x)
        {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
          //  anim.SetBool("IDLE", false);
          //  anim.SetBool("WALK", true);
        }

        if (Vector2.Distance(transform.position, PlayerTransform.transform.position) > 15f) states = States.Looking;
        if (Vector2.Distance(transform.position, PlayerTransform.transform.position) < 5f && podeAtacar) states = States.Ataque1;
    }

    public void Ataque1()
    {
        Debug.Log("Ataque 1");

        playerIsInRange = Physics2D.OverlapCircle(transform.position, bodyRadius, PlayerMask);
        StartCoroutine(Ataque11());
    }

    IEnumerator Ataque11()
    {
        //anim.SetBool("EsferaDeVida", playerIsInRange);
        yield return new WaitForSeconds(animationTime);
        //anim.SetBool("EsferaDeVida", false);
        if(playerIsInRange && podeAtacar)
        {
            PlayerTransform.GetComponent<PlayerHealth>().TakeDamage(damage);
            podeAtacar = false;
            states = States.Looking;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), bodyRadius);
    }

    public void Ataque2()
    {

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
