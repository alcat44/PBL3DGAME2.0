using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public GameObject pickupText;
    public AudioSource pickup;
    public int Index;  // Slot index of the item being picked up

    public bool interactable = false;

    void Pickup()
    {
        // Add the item to the inventory
        InventoryManager.Instance.Add(item);

        // Set the item index in InventoryManager
        InventoryManager.Instance.Index = InventoryManager.Instance.Items.Count - 1;

        // Destroy the picked-up item
        Destroy(gameObject);

        // Clear any instantiated items
        foreach (var instantiatedItem in InventoryManager.Instance.instantiatedItems)
        {
            if (instantiatedItem != null)
            {
                Destroy(instantiatedItem);
            }
        }
        InventoryManager.Instance.instantiatedItems.Clear();

        // Instantiate the item prefab if available
        if (item.prefab != null && InventoryManager.Instance.player != null)
        {
            InventoryManager.Instance.itemInstance = Instantiate(item.prefab, InventoryManager.Instance.player.position, InventoryManager.Instance.player.rotation);
            InventoryManager.Instance.itemInstance.transform.SetParent(InventoryManager.Instance.player);
            InventoryManager.Instance.equip = true;
            interactable = false;
            InventoryManager.Instance.instantiatedItems.Add(InventoryManager.Instance.itemInstance);
        }

        // Disable the ItemPickUp component on the newly instantiated item
        ItemPickUp itemPickUp = InventoryManager.Instance.itemInstance.GetComponent<ItemPickUp>();
        if (itemPickUp != null)
        {
            itemPickUp.enabled = false;
            itemPickUp.pickupText = pickupText;
            itemPickUp.pickupText.SetActive(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            pickupText.SetActive(true);
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            pickupText.SetActive(false);
            interactable = false;
        }
    }

    void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            pickupText.SetActive(false);
            interactable = false;
            Pickup();
        }
    }
}
