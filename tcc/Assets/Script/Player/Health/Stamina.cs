using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    // Stamina do Player
    public static float estamina;
    public static float staminaStay;
    // Ui Da Stamina
    public Stamina_UI staminaUI;
    public int damageInHealth;
    public float publicTimeToDie;
    float timeToDie;
    bool hasEndStamina;
    public static Stamina instance { get; private set; }

    private void Awake()
    {
        instance = this;
        estamina = GameManager.PlayerStamina;
        staminaStay = estamina;
        staminaUI.SetMaxStamina(staminaStay);
    }
    void Update()
    {
        if (GameManager.hasPassedTutorial)
        {
            staminaStay -= Time.deltaTime;

            staminaUI.SetStamina(staminaStay);

            if (staminaStay <= 0) timeToDie -= Time.time;

            if (timeToDie <= 0 && !hasEndStamina)
            {
                StartCoroutine(DamageHealth(damageInHealth));
                hasEndStamina = true;
            }

            Debug.Log("PublicTImeTOdie: " + timeToDie);
        }
    }

    public IEnumerator DamageHealth(int damage)
    {
        PlayerHealth.Instance.TakeDamage(damage);
        timeToDie = publicTimeToDie;

        yield return new WaitForSeconds(10);
        hasEndStamina = false;
    }

    public void UpStamina(float stamina)
    {
        staminaStay += stamina;
        if (staminaStay > estamina) staminaStay = estamina;
    }

    public void SetMaxStainaAfterSceneChange()
    {
        staminaStay = estamina;
        staminaUI.SetStamina(staminaStay);
    }
}
