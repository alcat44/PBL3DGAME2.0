using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public int Index;
    public Item item;
    public GameObject itemObject;
    private Vector3 itemPlacementPosition;
    
    //public GameObject dropText;
    public bool interactable = false;

    void Drop(int Index)
    {
        var item = InventoryManager.Instance.Items[Index];
        InventoryManager.Instance.Remove(item);
        Instantiate(itemObject, itemPlacementPosition, Quaternion.identity);
        Destroy(itemObject);
        //Destroy(gameObject);
        interactable = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("KerakTelorPlacement"))
        {
            if (InventoryManager.Instance.equip)
            {
                //dropText.SetActive(true);
                interactable = true;
                itemPlacementPosition = other.transform.position;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("KerakTelorPlacement"))
        {
            //dropText.SetActive(false);
            interactable = false;
        }
    }

    void Update()
    {
        itemObject = InventoryManager.Instance.itemInstance;
        Index = InventoryManager.Instance.Index;
        if (interactable && Input.GetKeyDown(KeyCode.F) && InventoryManager.Instance.equip)
        {
            //dropText.SetActive(false);
            interactable = false;
            Drop(Index);
            InventoryManager.Instance.equip = false;
        }
    }
}
