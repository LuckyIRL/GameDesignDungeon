using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<AllItems> inventoryItems = new List<AllItems>(); // Our items in the inventory

    private int numberOfKeys = 0;
    public TextMeshPro keyText;
    public int numberOfArrows = 0;

    private void Awake()
    {
        instance = this;
    }

    public void AddItem(AllItems item) // Add items to the inventory
    {
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Add(item);
        }
    }

    public void RemoveItem(AllItems item) // Remove items from the inventory
    {
        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
        }
    }

    public enum AllItems // All available items in the game
    {
        KeyRed,
        KeyYellow,
        KeyGreen,
        Arrows,
    }
}
