using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aranha_Ataque : MonoBehaviour
{
    bool canAttack;
    public int damage;
    public PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, ENemyBasicMovement.PlayerTransform.position) < 1f && canAttack)
        {
            if (playerHealth.hasShildUp == true)
            {
                Debug.Log("SHIELD");
                playerHealth.shieldBroken = true;
                playerHealth.hasShildUp = false;
                canAttack = false;
            }
            else
            {
                playerHealth.TakeDamage(damage);
                canAttack = false;
                if (!playerHealth.hasbeenPoisoned)
                {
                    playerHealth.poisoned = true;
                    playerHealth.hasbeenPoisoned = true;
                }
                if (playerHealth.hasbeenPoisoned) playerHealth.HitsPoisoned += 1;
            }
        }

        if (Vector2.Distance(transform.position, ENemyBasicMovement.PlayerTransform.position) > 1f) canAttack = false;
    }



    public void SetAttackTrue()
    {
        canAttack = true;
    }
}
