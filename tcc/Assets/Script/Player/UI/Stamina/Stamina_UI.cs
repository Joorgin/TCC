using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stamina_UI : MonoBehaviour
{
    public Slider staminaSlider;
    public void SetMaxStamina(float maxHealth)
    {
        staminaSlider.maxValue = maxHealth;
        staminaSlider.value = maxHealth;
    }

    public void SetStamina(float health)
    {
        staminaSlider.value = health;
    }
}
