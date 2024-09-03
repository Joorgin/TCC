using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        m_List.Add(this);
    }

    void Start()
    {
        switch (Contador_De_Tempo.DificuldadePorTempo)
        {
            case 1:
                Maxhealth = 130;
                Debug.Log("Enemy MaxHealth: " + Maxhealth);
                break;
            case 2:
                Maxhealth = 160;
                Debug.Log("Enemy MaxHealth: " + Maxhealth);
                break;
            case 3:
                Maxhealth = 200;
                Debug.Log("Enemy MaxHealth: " + Maxhealth);
                break;
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

    public void TakeDamage(float damage)
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
            fleashMaterialScript.Flash();
        }
    }

    public void Death()
    {
        Contador_de_Almas.instance.AlmentarAlmas(1);
        Instantiate(Bracelete, gameObject.transform.position, Quaternion.identity);
        //dá find e tira da lista
        Destroy(gameObject);
    }

    
}
