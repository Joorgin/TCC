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
            timeToDie = 1;
        }
    }

    public void Death()
    {
        Contador_de_Almas.instance.AlmentarAlmas(1);
        Instantiate(Bracelete, gameObject.transform.position, Quaternion.identity);
        //d� find e tira da lista
        Destroy(gameObject);
    }

    
}
