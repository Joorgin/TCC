using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int LastMap, MapsPassed;
    public static bool LastMapPassed;
    public static int NumberOfSouls;
    public static bool upgradeLevel, UpgradeLevelStamina;
    public static int CurrentLevelItemUpgrade = 1, CurrentLevelItemStaminaUpgrade = 1;
    public static int BraceletesNecessariosPorBau;
    public static string LastMapName;
    public static bool IsInMainScene, isInConversation, isInTutorial, hasPassedTutorial;

    #region Player 
    public static int PlayerMaxhealth = 100;
    public static float PlayerStamina = 180;
    #endregion

    #region camera
    // configura o limite que a camera do player pode ir
    public GameObject cinemachine;
    #endregion

    public Transform pfDamagePopUp;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

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

    public void Menu()
    {
        // Salvar Jogo
    }

    public void SetCamera()
    {
        cinemachine = GameObject.FindGameObjectWithTag("Camera");
        cinemachine.GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag("Player").transform;
        cinemachine.GetComponent<CinemachineVirtualCamera>().LookAt = GameObject.FindGameObjectWithTag("Player").transform;
        cinemachine.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GameObject.FindGameObjectWithTag("CameraConfiner").GetComponent<PolygonCollider2D>();
    }
}
