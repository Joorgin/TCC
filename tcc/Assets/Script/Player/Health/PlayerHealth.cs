using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    
    public static int Maxhealth;
    public int Currenthealth;

    public PlayerHealthUI healthUI;

    // Tudo sobre o shield e sua relacao com a vida
    public static bool hasShildUp, canShield, shieldBroken;
    public GameObject shield;
    float TimeToReDo;
    public static float TimeToShieldRemake = 20f;

    // tudo sobre o espelho e sua realcao com a vida
    public static bool hasArmorUp;
    public static int percentOfProtection;


    public static bool isAlive;

    public GameObject DeathPanel;

    public static int HealthRegen;
    public float TimetoRegenarateHealth;

   
    bool canTakeaDamage = true;
    public float TimeToTakeDamage;

    public static bool setMaxHealth;

    

    void Start()
    {

        Maxhealth = GameManager.PlayerMaxhealth;
        Debug.Log(Maxhealth);
        Currenthealth = Maxhealth;
        HealthRegen = 2;
        TimetoRegenarateHealth = 3.0f;
        healthUI.SetMaxHealth(Maxhealth);
        isAlive = true;
    }

    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.R) && canShield && !shieldBroken) 
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

        if(shieldBroken) 
        { 
            TimeToReDo += Time.deltaTime;

            if(TimeToReDo >= TimeToShieldRemake)
            {
                shieldBroken = false;
            }
        }

       if(Currenthealth <= 0)
        {
            Dead();
        }

       if(isAlive)
        {
            TimetoRegenarateHealth -= Time.deltaTime;
            if(TimetoRegenarateHealth <= 0f && Currenthealth < Maxhealth)
            {
                Currenthealth += HealthRegen;
                if(Currenthealth > Maxhealth) { Currenthealth = Maxhealth; }
                healthUI.SetHealth(Currenthealth);
                TimetoRegenarateHealth = 1.5f;
            }
        }

       if(!canTakeaDamage)
        {
            TimeToTakeDamage += Time.deltaTime;

            if(TimeToTakeDamage >= 1f)
            {
                canTakeaDamage = true;
            }
        }

       if(setMaxHealth)
        {
            healthUI.SetMaxHealth(Maxhealth);
            setMaxHealth = false;
        }

    }

    public void TakeDamage(int damage)
    {
        if (canTakeaDamage)
        {
            if(hasArmorUp) 
            {
                int damageReflet = (damage / 100) * percentOfProtection;
                damage -= damageReflet;
                Currenthealth -= damage;
                healthUI.SetHealth(Currenthealth);
                canTakeaDamage = false;
            }
            else
            {
                Currenthealth -= damage;
                healthUI.SetHealth(Currenthealth);
                canTakeaDamage = false;
            }
            
        }
    }

    public static void LibertarKiumbas()
    {
        Maxhealth += 25;
        setMaxHealth = true;
    }

    public static void ArmorUP()
    {
        hasArmorUp = true;
        percentOfProtection += 2;
    }

    public static void SetHabilitStatus()
    {
        float TimePercentOfShield = (TimeToShieldRemake / 100) * 10;

        if(TimeToShieldRemake > 10f) TimeToShieldRemake -= TimePercentOfShield;
    }


public void CurarPorRespown()
    {
        Currenthealth = Maxhealth;
        healthUI.SetHealth(Currenthealth);
    }

    public void Dead()
    {
        isAlive = false;
        StartCoroutine(ShowUI("MainScene"));
    }

    public IEnumerator ShowUI(string scene)
    {
        yield return new WaitForSeconds(1f);
        FadeScript.ShowUI();
        yield return new WaitForSeconds(1f);
        PlayerMovement.isInFinalScene = true;   
        SceneManager.LoadScene(scene);
    }
}
