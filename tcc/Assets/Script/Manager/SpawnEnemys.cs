using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform SpawnPositions;
    public float TimerToSpawn;
    public Transform TEMPPOSITION;
    public Vector3 spawmTEMP;
    void Update()
    {
        TimerToSpawn += Time.deltaTime;

        if (TimerToSpawn > 10)
        {
            int RandomNumber = Random.Range(0, enemies.Length);
            int RandomSpawn = Random.Range(0, 2);
            SpawnPositions = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            spawmTEMP = SpawnPositions.position;
            if (RandomSpawn == 0) spawmTEMP += new Vector3(-17.475f,  SpawnPositions.position.y);
            if (RandomSpawn == 1) spawmTEMP += new Vector3(17.471f,  SpawnPositions.position.y);

            
            TEMPPOSITION.position = spawmTEMP;

           GameObject temp =  Instantiate(enemies[RandomNumber], TEMPPOSITION);
            temp.transform.SetParent(null);
            temp.transform.position = spawmTEMP;

            spawmTEMP = SpawnPositions.position;
            TimerToSpawn = 0;
        }

    }
}
