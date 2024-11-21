using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIrd_Boss_Health : MonoBehaviour
{
    public static BIrd_Boss_Health instance;
    static List<BIrd_Boss_Health> m_List = new List<BIrd_Boss_Health>();

    public int Maxhealth;
    public float Currenthealth;

    public EnemyHealthUi healthUI;

    public GameObject Bracelete;
    public static bool isAlive;
    float timeToDie;

    // Onde se encaixa o flashSprite
    public FleashMaterial fleashMaterialScript;

    // Quanto de stamina o inimigo consede
    [Space]
    public float stamina;

    private void Awake()
    {
        m_List.Add(this);
        instance = this;
    }

    void Start()
    {
        Currenthealth = Maxhealth;
        healthUI.SetMaxHealth(Maxhealth);
        isAlive = true;

    }

    private void FixedUpdate()
    {
        if (timeToDie <= 0)
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
        Contador_de_Almas.instance.AlmentarAlmas(5);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(Bracelete, gameObject.transform.position, Quaternion.identity);
        }
        //dá find e tira da lista
        SceneChange.HasdefeatedBoss = true;
        Stamina.instance.UpStamina(stamina);
        Destroy(gameObject);
    }
}
