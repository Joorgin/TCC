using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Mal_Health : MonoBehaviour
{
    static List<Player_Mal_Health> m_List = new List<Player_Mal_Health>();

    public int Maxhealth;
    public float Currenthealth;

    public EnemyHealthUi healthUI;

    public GameObject Bracelete;
    public static bool isAlive;
    float timeToDie;

    // Onde se encaixa o flashSprite
    public FleashMaterial fleashMaterialScript;

    private void Awake()
    {
        m_List.Add(this);
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
        Destroy(gameObject);
    }

    public void RecuperarVida(int damage)
    {
        Currenthealth += damage;
        healthUI.SetHealth(Currenthealth);
    }
}
