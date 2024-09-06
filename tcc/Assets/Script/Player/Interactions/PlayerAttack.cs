using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    public float timeBtwAttack, startTimeBtwAttack;//para manipular o tempo de ataque
    public Transform attackPos, attackPos2;//para as direções de ataque
    public float attackRange;
    public LayerMask WhatIsEnemies;
    public LayerMask WhatIsEnemies2;

    public static float Damage;
    Collider2D[] enemiesToDamage;
    Collider2D[] enemiesToDamage2;

    public int weaponIsUsing;
    public GameObject Arrow;
    bool hasShoot;

    // Tudo sobre a flecha e sua relacao com dono
    [Space]
    [Header("CoolDown para atirar a flecha")]
    public static float cooldownForFlecha = 20f;

    // Porcentagem sobre o crit do ataque do player
    public static int CritPercent;
    bool Crited;

    // combo System
    [Space]
    [Header("Sistema de Combo e animacao")]
    public Animator anim;
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 0.9f;
    bool canAtack = true;
    // freeze no momento do ataque
    [Space]
    [Header("Freeze When Attack")]
    public float durationFreeze;
    bool _isFrozen = false;
    float _pendingFreezeDuration = 0f;
    bool _isThereMonsters;


    private void Start()
    {
        Damage = 10;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        bool direcaoVerticalMove;

        if (!Player_Type_2_Movement.isInMainScene) direcaoVerticalMove = PlayerMovement.verticalMove == 1;
        else direcaoVerticalMove = Player_Type_2_Movement.verticalMove == 1;

        attackPos.gameObject.SetActive(direcaoVerticalMove);
        attackPos2.gameObject.SetActive(!direcaoVerticalMove);
        Vector3 ataquePosicao = direcaoVerticalMove ? attackPos.position : attackPos2.position;
        enemiesToDamage = Physics2D.OverlapCircleAll(ataquePosicao, attackRange, WhatIsEnemies);
        enemiesToDamage2 = Physics2D.OverlapCircleAll(ataquePosicao, attackRange, WhatIsEnemies2);

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if (PlayerHealth.isAlive || Player_Type_2_Movement.isInMainScene)
        {
            Atacar(direcaoVerticalMove);
        }

        if (_pendingFreezeDuration > 0f && !_isFrozen)
        {
            
            StartCoroutine(SetTimeForAtackEffect());
        }
    }
    /// <summary>
    /// Ataca quando tempo entre os ataques for menor que zero
    /// Depende da direcao do ataque
    /// </summary>
    void Atacar(bool direcao)
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X)) 
            && (enemiesToDamage != null || enemiesToDamage2 != null) && noOfClicks == 0 && canAtack)
        {
            
            lastClickedTime = Time.time;
            noOfClicks++;

            if (noOfClicks >= 1)
            {

                switch (weaponIsUsing)
                {
                    case 0:
                        anim.SetBool("Attack1", true);
                        //anim.SetTrigger("Attack");
                        PlayerMovement.isAttacking = true;
                        StartCoroutine(AttackHand1(direcao));
                        timeBtwAttack = startTimeBtwAttack;
                        canAtack = false;
                        break;
                }
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 2);
        }
        else if((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X)) &&
            (enemiesToDamage != null || enemiesToDamage2 != null) && noOfClicks == 1) noOfClicks++;

        else if (Input.GetKey(KeyCode.Q) && !hasShoot)
        {
            //enquanto nao houver animacao manter isAttacking comentado
            // PlayerMovement.isAttacking = true;
            StartCoroutine(SHOOTARROW());
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }



    // Enumerator para o primeiro soco detectando quantos e quais inimigos estao na range do player
    public IEnumerator AttackHand1(bool direcao)
    {
       

        int percentForCrit = Random.Range(0, 100);
        if (percentForCrit <= CritPercent)
        {
            Crited = true;
            Damage *= 2;
        }
        foreach (Collider2D etd in enemiesToDamage)
        {
            //basic Enemy
            if (etd.GetComponent<ENemyBasicMovement>() != null)
            {
                etd.GetComponent<EnemyHeatlh>().TakeDamage(Damage);
                etd.GetComponent<ENemyBasicMovement>().KBCounter = etd.GetComponent<ENemyBasicMovement>().KBTotalTime;
                etd.GetComponent<ENemyBasicMovement>().KBForce = 1;
                etd.GetComponent<ENemyBasicMovement>().KnockFromRight = !direcao;//inverte pq direcao foi feito com PlayerMovement.verticalMove == 1
                _isThereMonsters = true;
                durationFreeze = 0.1f;
            }

            //Explosive Enemy
            if (etd.GetComponent<ExplosiveEnemyMovement>() != null)
            {
                etd.GetComponent<ExplosiveEnemyHealth>().TakeDamage(Damage);
                etd.GetComponent<ExplosiveEnemyMovement>().KBCounter = etd.GetComponent<ExplosiveEnemyMovement>().KBTotalTime;
                etd.GetComponent<ExplosiveEnemyMovement>().KBForce = 1;
                etd.GetComponent<ExplosiveEnemyMovement>().KnockFromRight = !direcao;
                _isThereMonsters = true;
                durationFreeze = 0.1f;
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
        if (noOfClicks >= 2)
        {
            anim.SetBool("Attack2", true);
            StartCoroutine(AttackHand2(direcao));
        }
        else
        {
            anim.SetBool("Attack1", false);
            noOfClicks = 0;
            PlayerMovement.isAttacking = false;
        }
    }
    // Funcao para o segundo soco detectando quantos e quais inimigos estao na range do player
    public IEnumerator AttackHand2(bool direcao)
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
                _isThereMonsters = true;
                durationFreeze = 0.3f;
            }

            //ExplosiveEnemy 
            if (etd.GetComponent<ExplosiveEnemyMovement>() != null)
            {
                etd.GetComponent<ExplosiveEnemyHealth>().TakeDamage(Damage);
                etd.GetComponent<ExplosiveEnemyMovement>().KBCounter = etd.GetComponent<ExplosiveEnemyMovement>().KBTotalTime;
                etd.GetComponent<ExplosiveEnemyMovement>().KBForce = 1;
                etd.GetComponent<ExplosiveEnemyMovement>().KnockFromRight = !direcao;
                _isThereMonsters = true;
                durationFreeze = 0.3f;
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
        if (Crited)
        {
            Damage -= Damage / 2;
            Crited = false;
        }
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Attack1", false);
        anim.SetBool("Attack2", false);
        PlayerMovement.isAttacking = false;
        noOfClicks = 0;
        canAtack = true;
    }

    public void Freeze()
    {
        _pendingFreezeDuration = durationFreeze;
    }

    public void SeeIfThereIsAMonster()
    {
        if (_isThereMonsters) Freeze();

        _isThereMonsters = false;
    }

    IEnumerator SetTimeForAtackEffect()
    {
        _isFrozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(durationFreeze);
        Time.timeScale = original;
        _pendingFreezeDuration = 0f;
        _isFrozen = false;
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

        yield return new WaitForSeconds(cooldownForFlecha);
        hasShoot = false;

    }

    public static void setHabilitStatus()
    {
        float cooldownPercent = (cooldownForFlecha / 100) * 10;

        if (cooldownForFlecha >= 14f) cooldownForFlecha -= cooldownPercent;

        Debug.Log("Time da flecha:" + cooldownForFlecha);
    }

    // Aumenta a chance de Critico do Player
    public static void SetCritChance()
    {
        CritPercent += 5;
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
