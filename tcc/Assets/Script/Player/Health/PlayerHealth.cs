using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    
    public static int Maxhealth;
    public int Currenthealth;

    public PlayerHealthUI healthUI;

    public static bool hasShildUp;
    public GameObject shield;

    public static bool isAlive;

    public GameObject DeathPanel;

    public static int HealthRegen;
    public float TimetoRegenarateHealth;

    public static bool canShield;
    public static bool shieldBroken;
    float TimeToReDo;
    public static float TimeToShieldRemake = 20f;
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
            Currenthealth -= damage;
            healthUI.SetHealth(Currenthealth);
            canTakeaDamage = false;
        }
    }

    public static void LibertarKiumbas()
    {
        Maxhealth += 25;
        setMaxHealth = true;
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
