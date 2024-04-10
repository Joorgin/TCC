using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstantiate : MonoBehaviour
{
    public GameObject InstantiatePrefab;

    private void Awake()
    {
        Instantiate(InstantiatePrefab, gameObject.transform.position, Quaternion.identity);
    }
}
