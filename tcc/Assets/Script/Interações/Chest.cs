using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    
    public string ChestID;
    public string Rarity;
    public GameObject[] ItemToDrop;

    public static bool isInRange;

    private void Awake()
    {
        ChestID = gameObject.name;
    }

    private void Update()
    {
        if(isInRange) 
        { 
          if(PlayerMovement.ChestName == ChestID) 
            {

                if (Input.GetKeyDown(KeyCode.E))
                {
                    int RandomNumber = Random.Range(0, ItemToDrop.Length);

                    switch (RandomNumber)
                    {
                        case 0:
                            Instantiate(ItemToDrop[RandomNumber], gameObject.transform.position, Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(ItemToDrop[RandomNumber], gameObject.transform.position, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(ItemToDrop[RandomNumber], gameObject.transform.position, Quaternion.identity);
                            break;


                    }
                }
            
            }
        
        }
    }


}
