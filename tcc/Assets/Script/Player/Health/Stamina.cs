using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    // Stamina do Player
    public static float stamina = 60f;

    // Ui Da Stamina
    public Stamina_UI staminaUI;
    private void Start()
    {
        staminaUI.SetMaxStamina(stamina);
    }
    void Update()
    {
        stamina -= Time.deltaTime;

        staminaUI.SetStamina(stamina);

        if (stamina <= 0) PlayerHealth.deadByStamina = true;
    }
}
