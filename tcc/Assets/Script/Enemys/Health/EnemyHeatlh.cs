using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHeatlh : MonoBehaviour
{
    static List<EnemyHeatlh> m_List = new List<EnemyHeatlh>();

    public int Maxhealth;
    public float Currenthealth;

    public EnemyHealthUi healthUI;

    public GameObject Bracelete;
    bool isAlive;
    float timeToDie;

    // Onde se encaixa o flashSprite
    public FleashMaterial fleashMaterialScript;

    // Quanto de stamina o inimigo consede
    [Space]
    public float stamina;

    public static bool canGrowHealth;

    private void Awake()
    {
        m_List.Add(this);
    }

    void Start()
    {
        if(canGrowHealth) 
        {
            Maxhealth += 30;
            canGrowHealth = false;
        }
        Currenthealth = Maxhealth;
        healthUI.SetMaxHealth(Maxhealth);
        isAlive = true;

    }

    private void FixedUpdate()
    {
        if(timeToDie <= 0) 
            return;
        else
            timeToDie -= Time.deltaTime;

        
    }

    public void TakeDamage(int damage)
    {
        if (timeToDie <= 0)
        {
            Currenthealth -= damage;
            healthUI.SetHealth(Currenthealth);
            if (Currenthealth <= 0 && isAlive)
            {
                Death();
                isAlive = false;
            }
            timeToDie = 0.5f;
            DamagePopUp.Create(gameObject.transform.position, damage);
            fleashMaterialScript.Flash();
        }
    }

    public void Death()
    {
        Contador_de_Almas.instance.AlmentarAlmas(1);
        Instantiate(Bracelete, gameObject.transform.position, Quaternion.identity);
        Stamina.instance.UpStamina(stamina);
        Destroy(gameObject);
    }

    
}
