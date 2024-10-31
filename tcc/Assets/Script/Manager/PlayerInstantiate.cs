using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstantiate : MonoBehaviour
{
    public Transform InstantiatePrefabPosition;
    public GameObject InstantiatePrefab2;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(InstantiatePrefab2, InstantiatePrefabPosition.transform.position, Quaternion.identity);
            GameManager.ChangePlayerPosition(InstantiatePrefabPosition);
        }
        else
        {
            GameManager.ChangePlayerPosition(InstantiatePrefabPosition);
        }
    }
}
