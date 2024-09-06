using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int LastMap, MapsPassed;
    public static bool LastMapPassed;
    public static int NumberOfSouls, NumberOfSoulsStamina;
    public static bool upgradeLevel, UpgradeLevelStamina;
    public static int CurrentLevelItemUpgrade, CurrentLevelItemStaminaUpgrade = 1;
    public static int BraceletesNecessariosPorBau;
    public static string LastMapName;

    #region Player 
    public static int PlayerMaxhealth = 100;
    public static float PlayerStamina = 60;
    #endregion

    public Transform pfDamagePopUp;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

       // if(LastMapPassed)
       //     Destroy(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("GAME MANAGER");
        }
    }

    public static void ChangePlayerPosition(Transform position)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = position.position;
    }
}
