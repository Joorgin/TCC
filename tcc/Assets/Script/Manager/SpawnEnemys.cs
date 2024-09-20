using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] SpawnPositions;
    public float TimerToSpawn;
    void Update()
    {
        TimerToSpawn += Time.deltaTime;

        if (TimerToSpawn > 10)
        {
            int RandomNumber = Random.Range(0, enemies.Length);
            int RandomSpawn = Random.Range(0, SpawnPositions.Length);

            GameObject temp = Instantiate(enemies[RandomNumber], SpawnPositions[RandomSpawn].transform.position, Quaternion.identity);
            temp.transform.SetParent(null);
            TimerToSpawn = 0;
        }

    }
}
