using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    private List<GameObject> instantiatedItems = new List<GameObject>();

    public Transform ItemContent;
    public GameObject InventoryItem, Inventory;
    public Transform player;
    public bool isInventoryOpen = false;

    private void Awake() 
    {
        Instance = this;
        Inventory.SetActive(false);
    }

    public void Add(Item item)
    {
        Items.Add(item);
        ListItems();
    }

    public void ListItems()
    {
        foreach(Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemNo = obj.transform.Find("ItemNo").GetComponent<TextMeshProUGUI>();
        
            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            itemNo.text = item.id.ToString();
        }
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        

        // Handle shortcut keys 1-10
        for (int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                UseItem(i);
            }
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Unequip();
            }
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        Inventory.SetActive(isInventoryOpen);
    }

    private void UseItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= Items.Count)
        {
            Debug.LogError("Invalid inventory slot index.");
            return;
        }

        var item = Items[slotIndex];
        if (item != null)
        {
            // Implement item usage logic here
            Debug.Log($"Using item: {item.itemName}");

            foreach (var instantiatedItem in instantiatedItems)
            {
                if (instantiatedItem != null)
                {
                    Destroy(instantiatedItem);
                }
            }
            instantiatedItems.Clear();

            // Instantiate the 3D object and attach it to the player
            if (item.prefab != null && player != null)
            {
                GameObject itemInstance = Instantiate(item.prefab, player.position, player.rotation);
                itemInstance.transform.SetParent(player);

                // Add the new item to the list
                instantiatedItems.Add(itemInstance);
            }
            else
            {
                Debug.LogError("Item prefab or spawn point not set.");
            }
        }
    }

    private void Unequip()
    {
        foreach (var instantiatedItem in instantiatedItems)
            {
                if (instantiatedItem != null)
                {
                    Destroy(instantiatedItem);
                }
            }
        instantiatedItems.Clear();
    }
}
