using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    public float timeBtwAttack,startTimeBtwAttack;//para manipular o tempo de ataque
    public Transform attackPos, attackPos2;//para as direções de ataque
    public float attackRange;
    public LayerMask WhatIsEnemies;
    public LayerMask WhatIsEnemies2;

    public static float Damage;
    Collider2D[] enemiesToDamage;
    Collider2D[] enemiesToDamage2;
    public Animator anim;

    public int weaponIsUsing;
    public GameObject Arrow;
    bool hasShoot;

    private void Start()
    {
        Damage = 10;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        bool direcaoVerticalMove = PlayerMovement.verticalMove == 1;
        attackPos.gameObject.SetActive(direcaoVerticalMove);
        attackPos2.gameObject.SetActive(!direcaoVerticalMove);
        Vector3 ataquePosicao = direcaoVerticalMove ? attackPos.position : attackPos2.position;
        enemiesToDamage = Physics2D.OverlapCircleAll(ataquePosicao, attackRange, WhatIsEnemies);
        enemiesToDamage2 = Physics2D.OverlapCircleAll(ataquePosicao, attackRange, WhatIsEnemies2);

        if (PlayerHealth.isAlive)
        {
            Atacar(direcaoVerticalMove);
        }
    }
    /// <summary>
    /// Ataca quando tempo entre os ataques for menor que zero
    /// Depende da direcao do ataque
    /// </summary>
    void Atacar(bool direcao)
    {
        if (timeBtwAttack < 0)
        {
            if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X)) && (enemiesToDamage != null || enemiesToDamage2 !=null))
            {
                switch (weaponIsUsing)
                {
                    case 0:
                         anim.SetTrigger("Attack");
                         PlayerMovement.isAttacking = true;
                         StartCoroutine(AttackHand1(direcao));
                         timeBtwAttack = startTimeBtwAttack;
                        break;
                }
        
            }
            else if(Input.GetKey(KeyCode.Q) && (enemiesToDamage != null || enemiesToDamage2 != null))
            {
                //enquanto nao houver animacao manter isAttacking comentado
               // PlayerMovement.isAttacking = true;
                StartCoroutine(SHOOTARROW());
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    // Enumerator para o primeiro soco detectando quantos e quais inimigos estao na range do player
    public IEnumerator AttackHand1(bool direcao)
    {
        foreach (Collider2D etd in enemiesToDamage)
        {
            //basic Enemy
            if (etd.GetComponent<ENemyBasicMovement>() != null)
            {
                etd.GetComponent<EnemyHeatlh>().TakeDamage(Damage);
                etd.GetComponent<ENemyBasicMovement>().KBCounter = etd.GetComponent<ENemyBasicMovement>().KBTotalTime;
                etd.GetComponent<ENemyBasicMovement>().KBForce = 1;
                etd.GetComponent<ENemyBasicMovement>().KnockFromRight = !direcao;//inverte pq direcao foi feito com PlayerMovement.verticalMove == 1
            }

            //Explosive Enemy
            if (etd.GetComponent<ExplosiveEnemyMovement>() != null)
            {
                etd.GetComponent<ExplosiveEnemyHealth>().TakeDamage(Damage);
                etd.GetComponent<ExplosiveEnemyMovement>().KBCounter = etd.GetComponent<ExplosiveEnemyMovement>().KBTotalTime;
                etd.GetComponent<ExplosiveEnemyMovement>().KBForce = 1;
                etd.GetComponent<ExplosiveEnemyMovement>().KnockFromRight = !direcao;
            }

            //Flying Enemy
            //etd.GetComponent<FlyingEnemy>().KBCounter = etd.GetComponent<FlyingEnemy>().KBTotalTime;
           // etd.GetComponent<FlyingEnemy>().KnockFromRight = !direcao;

        }

        foreach (Collider2D etd in enemiesToDamage2)
        {
            if (etd.GetComponent<FlyingBirdboss>() != null)
            {
                etd.GetComponent<BIrd_Boss_Health>().TakeDamage(Damage);
            }
        }

        yield return new WaitForSeconds(0.5f);
        AttackHand2(direcao);
    }
    // Funcao para o segundo soco detectando quantos e quais inimigos estao na range do player
    public void AttackHand2(bool direcao)
    {
        foreach (Collider2D etd in enemiesToDamage)
        {

            //Basic Enemy
            if (etd.GetComponent<ENemyBasicMovement>() != null)
            {
                etd.GetComponent<EnemyHeatlh>().TakeDamage(Damage);
                etd.GetComponent<ENemyBasicMovement>().KBCounter = etd.GetComponent<ENemyBasicMovement>().KBTotalTime;
                etd.GetComponent<ENemyBasicMovement>().KBForce = 3;
                etd.GetComponent<ENemyBasicMovement>().KnockFromRight = !direcao;//inverte pq direcao foi feito com PlayerMovement.verticalMove == 1
            }

            //ExplosiveEnemy 
            if (etd.GetComponent<ExplosiveEnemyMovement>() != null)
            {
                etd.GetComponent<ExplosiveEnemyHealth>().TakeDamage(Damage);
                etd.GetComponent<ExplosiveEnemyMovement>().KBCounter = etd.GetComponent<ExplosiveEnemyMovement>().KBTotalTime;
                etd.GetComponent<ExplosiveEnemyMovement>().KBForce = 1;
                etd.GetComponent<ExplosiveEnemyMovement>().KnockFromRight = !direcao;
            }

            //FlyingEnemy
            //etd.GetComponent<FlyingEnemy>().KBCounter = etd.GetComponent<FlyingEnemy>().KBTotalTime;
            // etd.GetComponent<FlyingEnemy>().KnockFromRight = !direcao;

        }

        foreach (Collider2D etd in enemiesToDamage2)
        {
            if (etd.GetComponent<FlyingBirdboss>() != null)
            {
                etd.GetComponent<BIrd_Boss_Health>().TakeDamage(Damage);
            }
        }
    }
    //Enumerator para atirar a flecha apartir do ponto de ataque 
    public IEnumerator SHOOTARROW()
    {
        // toca a animacao de atirar a flecha
       // anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.5f);
        // Invoca a flecha
        
        if (PlayerMovement.verticalMove > 0 && !hasShoot)
        {
            Instantiate(Arrow, attackPos.transform.position, Quaternion.identity);
            hasShoot = true;
        }
        if (PlayerMovement.verticalMove < 0 && !hasShoot)
        {
            Instantiate(Arrow, attackPos2.transform.position, Quaternion.identity);
            hasShoot = true;
        }
        yield return new WaitForSeconds(0.5f);
        hasShoot = false;
    }

    /// Desenha um circulo em volta no attackPos.position
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.position, attackRange);
    }

    public static void SacrificarKiumbas()
    {
        Damage += 10;
        Debug.Log(Damage);
    }
}
