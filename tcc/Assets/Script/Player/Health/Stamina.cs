using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    // Stamina do Player
    public static float stamina = 240f;

    // Update is called once per frame
    void Update()
    {
        stamina -= Time.deltaTime;

        if (stamina <= 0) PlayerHealth.deadByStamina = true;
    }
}
