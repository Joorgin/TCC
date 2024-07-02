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
