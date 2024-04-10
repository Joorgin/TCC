using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float timeBtwAttack,startTimeBtwAttack;//para manipular o tempo de ataque
    public Transform attackPos, attackPos2;//para as direções de ataque
    public float attackRange;
    public LayerMask WhatIsEnemies;

    public float Damage;
    Collider2D[] enemiesToDamage;



    private void Update()
    {
        bool direcaoVerticalMove = PlayerMovement.verticalMove == 1;
        attackPos.gameObject.SetActive(direcaoVerticalMove);
        attackPos2.gameObject.SetActive(!direcaoVerticalMove);
        Vector3 ataquePosicao = direcaoVerticalMove ? attackPos.position : attackPos2.position;
        enemiesToDamage = Physics2D.OverlapCircleAll(ataquePosicao, attackRange, WhatIsEnemies);

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
            if (Input.GetKey(KeyCode.Mouse0) && enemiesToDamage != null)
            {
                Debug.Log("Damage");
                foreach (Collider2D etd in enemiesToDamage)
                {
                    etd.GetComponent<EnemyHeatlh>().TakeDamage(Damage);
                    etd.GetComponent<ENemyBasicMovement>().KBCounter = etd.GetComponent<ENemyBasicMovement>().KBTotalTime;
                    etd.GetComponent<FlyingEnemy>().KBCounter = etd.GetComponent<FlyingEnemy>().KBTotalTime;

                    etd.GetComponent<ENemyBasicMovement>().KnockFromRight = !direcao;//inverte pq direcao foi feito com PlayerMovement.verticalMove == 1
                    etd.GetComponent<FlyingEnemy>().KnockFromRight = !direcao;
                }
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

    }

    /// <summary>
    /// Desenha um circulo em volta no attackPos.position
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attackPos.position, attackRange);
    }

    public void SacrificarKiumbas()
    {
        Contador_de_Almas.instance.ZerarAlmas();
    }
}
