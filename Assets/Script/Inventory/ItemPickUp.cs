using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public GameObject pickupText, itemObject;
    public AudioSource pickup;
    public bool interactable;
    void Pickup()
    {
        InventoryManager.Instance.Add(item);
        Destroy(gameObject);
    }

    

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("MainCamera"))
        {
            pickupText.SetActive(true);
            interactable = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("MainCamera"))
        {
            pickupText.SetActive(false);
            interactable = false;
        }
    }
    void Update()
    {
        if(interactable == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                pickupText.SetActive(false);
                interactable = false;
                //pickup.Play();
                
                Pickup();
            }
        }
    }
}
