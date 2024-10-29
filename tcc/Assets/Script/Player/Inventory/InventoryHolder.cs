using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem inventorySystem;

    public InventorySystem InventorySystem => inventorySystem;
    public static InventoryHolder Instance { get; private set; }

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    private void Awake()
    {
        //inventorySystem = new InventorySystem(inventorySize);
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
        inventorySystem = new InventorySystem(inventorySize);
    }

}
