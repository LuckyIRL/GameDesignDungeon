using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using StarterAssets;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<AllItems> inventoryItems = new List<AllItems>(); // Our items in the inventory

    // Reference to the ThirdPersonController script
    //private ThirdPersonController thirdPersonController;

    //private int numberOfKeys = 0;
    [SerializeField] public TextMeshProUGUI keyText;
    [SerializeField] public TextMeshProUGUI arrowText;
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

    // method to update the key text and the number of arrows text
    public void UpdateUI()
    {
        int keyCount = CountKeys();
        keyText.text = "Keys: " + keyCount.ToString();
        arrowText.text = "Arrows: " + numberOfArrows.ToString();
    }

    private int CountKeys()
    {
        int count = 0;
        foreach (var item in inventoryItems)
        {
            if (item == AllItems.KeyRed || item == AllItems.KeyYellow || item == AllItems.KeyGreen)
            {
                count++;
            }
        }
        return count;
    }

    // Check if player has arrows in the inventory to shoot
    public bool HasArrows()
    {
        return numberOfArrows > 0;
    }


    public enum AllItems // All available items in the game
    {
        KeyRed,
        KeyYellow,
        KeyGreen,
        Arrows,
    }
}
