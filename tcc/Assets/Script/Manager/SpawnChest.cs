using UnityEngine;

public class SpawnChest : MonoBehaviour
{
    public int NumberOfChests;
    public GameObject prefabToInstantiate;
    public Transform[] spawnPositions;
    public bool[] positionUsed;

    void Start()
    {
        positionUsed = new bool[spawnPositions.Length];
        for(int i = 0; i < NumberOfChests; i++)
         {
            InstantiateObjectAtRandomPosition();
            prefabToInstantiate.name = prefabToInstantiate.name + 1;
         }
    }

   

    void InstantiateObjectAtRandomPosition()
    {
        int randomIndex = GetRandomUnusedPositionIndex();

        if (randomIndex != -1)
        {
            // Mark the position as used
            positionUsed[randomIndex] = true;

            // Instantiate the object at the chosen position
            Instantiate(prefabToInstantiate, spawnPositions[randomIndex].position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("All positions are already used!");
        }
    }

    int GetRandomUnusedPositionIndex()
    {
        // Create a list of available indices
        System.Collections.Generic.List<int> availableIndices = new System.Collections.Generic.List<int>();

        // Find available indices (positions that are not yet used)
        for (int i = 0; i < positionUsed.Length; i++)
        {
            if (!positionUsed[i])
            {
                availableIndices.Add(i);
            }
        }

        // If there are available positions, return a random one
        if (availableIndices.Count > 0)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            return availableIndices[randomIndex];
        }
        else
        {
            // If all positions are used, return -1
            return -1;
        }
    }
}
