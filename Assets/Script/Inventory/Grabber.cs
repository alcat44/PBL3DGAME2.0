using UnityEngine;

public class Grabber : MonoBehaviour
{
    private GameObject selectedObject;
    public GameObject pickupText, dropText;
    public Transform playerHand; // Reference to the player's hand transform
    public bool isDroppingAtTrigger = false;
    private Vector3 triggerPosition;
    public Vector3 baju1Position, baju2Position, baju3Position;
    public GameObject baju1, baju2, baju3, fotoMakanan;
    public bool fotoMakananInstantiated = false;

    void OnTriggerStay(Collider other)
    {
        if (selectedObject != null && other.CompareTag("BajuPlacement"))
        {
            Debug.Log("Bisa didrop");
            pickupText.SetActive(true);
            triggerPosition = other.transform.position;
            isDroppingAtTrigger = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (selectedObject != null && other.CompareTag("BajuPlacement"))
        {
            
            pickupText.SetActive(false);
            isDroppingAtTrigger = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("drag"))
                    {
                        pickupText.SetActive(false);
                        return;
                    }
                    else
                    {
                        pickupText.SetActive(true);
                    }
                    
                    selectedObject = hit.collider.gameObject;
                    selectedObject.transform.SetParent(playerHand);
                    selectedObject.transform.localPosition = Vector3.zero;
                }
            }
            else
            {
                if (isDroppingAtTrigger)
                {
                    Debug.Log("Dropping at trigger position.");
                    selectedObject.transform.position = triggerPosition;
                    
                    DropSelectedObject();
                    isDroppingAtTrigger = false;
                }
                else
                {
                    Debug.Log("Dropping at current position.");
                    Vector3 currentPosition = selectedObject.transform.position;
                    currentPosition.y = 3.5f;
                    selectedObject.transform.position = currentPosition;
                    DropSelectedObject();
                }
            }
            
            
        }

        
        CheckPositions();
        
        
    }

    private RaycastHit CastRay()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        return hit;
    }

    private void DropSelectedObject()
    {
        if (selectedObject != null)
        {
            selectedObject.transform.rotation = Quaternion.identity;
            selectedObject.transform.SetParent(null);
            selectedObject = null;
        }
    }

    private void CheckPositions()
    {
        if (!fotoMakananInstantiated && baju1 != null && baju2 != null && baju3 != null)
        {
            if (Vector3.Distance(baju1.transform.position, baju1Position) < 0.1f &&
                Vector3.Distance(baju2.transform.position, baju2Position) < 0.1f &&
                Vector3.Distance(baju3.transform.position, baju3Position) < 0.1f)
            {
                Debug.Log("All objects are in position. Instantiating new object.");
                Instantiate(fotoMakanan, new Vector3(57, 5, -3), Quaternion.Euler(90, 0, 45));
                fotoMakananInstantiated = true;
            }
        }
    }
}
