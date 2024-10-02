using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCampoDeBatalha : MonoBehaviour
{
    public int Id;
    public GameObject campoDeBatalha;

    private void Start()
    {
        if (Id == SpawnPortal.RandomIndex)
        {
            campoDeBatalha.SetActive(true);
        }
    }
}
