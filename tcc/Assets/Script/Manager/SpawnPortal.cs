using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortal : MonoBehaviour
{
    public static int RandomIndex;
    public Transform[] SpawnPositions;
    public GameObject Prefab;

    private void Awake()
    {
        RandomIndex = Random.Range(0, 4);
        Instantiate(Prefab, SpawnPositions[RandomIndex].transform.position, Quaternion.identity);
    }
}
