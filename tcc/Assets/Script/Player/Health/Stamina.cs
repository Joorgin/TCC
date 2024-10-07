using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    // Stamina do Player
    public static float stamina;
    public static float staminaStay;
    // Ui Da Stamina
    public Stamina_UI staminaUI;
    private void Start()
    {
        stamina = GameManager.PlayerStamina;
        staminaStay = stamina;
        staminaUI.SetMaxStamina(staminaStay);
        Debug.Log("Stamina: " + staminaStay);
    }
    void Update()
    {
        staminaStay -= Time.deltaTime;

        staminaUI.SetStamina(staminaStay);

        if (staminaStay <= 0)
        {
            PlayerHealth.deadByStamina = true;
            staminaStay = stamina;
        }
    }

    public static void UpStamina(float stamina)
    {
        staminaStay += stamina;
    }
}
