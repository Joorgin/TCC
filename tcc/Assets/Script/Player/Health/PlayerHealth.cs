using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public int Maxhealth;
    public int Currenthealth;

    public PlayerHealthUI healthUI;

    public static bool hasShildUp;
    public GameObject shield;

    public static bool isAlive;

    public GameObject DeathPanel;

    void Start()
    {
        Currenthealth = Maxhealth;
        healthUI.SetMaxHealth(Maxhealth);
        isAlive = true;
    }

    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.R)) 
        { 
          hasShildUp = true;
        }

        if(hasShildUp)
        {
            shield.SetActive(true);
        }
        if(!hasShildUp)
        {
            shield.SetActive(false);
        }

       if(Currenthealth <= 0)
        {
            Dead();
        }
    }

    public void TakeDamage(int damage)
    {
       Currenthealth -= damage;
       healthUI.SetHealth(Currenthealth);
    }

    public void LibertarKiumbas()
    {
        Maxhealth += 1 * Contador_de_Almas.currentCoins;
        Contador_de_Almas.instance.ZerarAlmas();
    }

    public void CurarPorRespown()
    {
        Currenthealth = Maxhealth;
        healthUI.SetHealth(Currenthealth);
    }

    public void Dead()
    {
        isAlive = false;
        DeathPanel.SetActive(true);
    }
}
