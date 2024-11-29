using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int estatuaLevel;
    public int numeroDeAlmas;
    public bool passouDoTutorial;

    public GameData (GameManager gameManager)
    {
        estatuaLevel = gameManager.CurrentLevelItemStaminaUpgrade;
        numeroDeAlmas = gameManager.NumberOfSouls;
        passouDoTutorial = gameManager.hasPassedTutorial;
    }

}
