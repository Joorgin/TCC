using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortal : MonoBehaviour
{
    public int RandomIndex;
    public Transform[] SpawnPositions;
    public GameObject Prefab;
    void Start()
    {
        RandomIndex = Random.Range(0, SpawnPositions.Length);
        Instantiate(Prefab, SpawnPositions[RandomIndex].transform.position, Quaternion.identity);
    }
}
