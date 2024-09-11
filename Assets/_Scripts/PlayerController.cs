/* PlayerController.cs
 * 
 *  This script controls all of the movement and interaction from the player
 */

using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("References")]
    Rigidbody rb;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;

    [Header("Interactions")]
    [SerializeField] List<GameObject> interactionsInRange = new List<GameObject>();
    [SerializeField] GameObject activeInteraction;

    [Header("Pick Up")]
    [SerializeField] bool isHoldingItem = false;
    [SerializeField] Transform itemHolder;

    [Header("POS System")]
    [SerializeField] Canvas canvas_POS;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        InputManager.Instance.Interacted += Interact;
    }

    private void FixedUpdate()
    {
        Move();

        ValidateInteractions();
    }

    #region OnTrigger Events
    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<Interactable>(out Interactable interactable))
        {
            if (!interactionsInRange.Contains(interactable.gameObject))
            {
                interactionsInRange.Add(interactable.gameObject); // Add the new interaction to the list
                Debug.Log($"Added {interactable.gameObject.name} to interactions list.");
            }

            GetMostRelevantInteraction(); // Update the most relevant interaction
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.TryGetComponent<Interactable>(out Interactable interactable))
        {
            if (interactionsInRange.Contains(interactable.gameObject))
            {
                interactionsInRange.Remove(interactable.gameObject); // Remove the interaction from the list
                Debug.Log($"Removed {interactable.gameObject.name} from interactions list.");
            }

            // Check and update activeInteraction if it is the one that was removed
            if (activeInteraction == interactable.gameObject)
            {
                activeInteraction = null; // Clear activeInteraction if it was the one removed
                GetMostRelevantInteraction(); // Update the most relevant interaction
            }
        }
    }
    #endregion

    void Move()
    {
        rb.velocity = new Vector3(InputManager.Instance.moveInput.x * moveSpeed * Time.fixedDeltaTime,
                                  rb.velocity.y,
                                  InputManager.Instance.moveInput.y * moveSpeed * Time.fixedDeltaTime);
    }

    void Interact()
    {
        if (activeInteraction != null)
        {
            if (activeInteraction.GetComponent<DropLocation>())
            {
                if (isHoldingItem)
                {
                    //Drop currently held item
                    DropItem(activeInteraction.GetComponent<DropLocation>().gameObject.transform);
                }
            }

            if (activeInteraction.GetComponent<FoodItem>())
            {
                //If item is food -> Pick up
                if (!isHoldingItem)
                {
                    PickupItem(activeInteraction);
                }
            }

            //Take the Order at the table
            if (activeInteraction.GetComponent<Table>())
            {
                Table table = activeInteraction.GetComponent<Table>();

                // Check if the player is holding an item and deliver it
                if (isHoldingItem)
                {
                    DeliverOrder(table);
                }
                else
                {
                    table.IsOrderTaken = true; // Take the order at the table if not holding any item
                    Debug.Log("Order taken at the table!");
                }
            }

            if (activeInteraction.GetComponent<OrderingSystem>())
            {
                Debug.Log("Interacting with POS system");
                if (canvas_POS.gameObject.activeSelf == false)
                {
                    canvas_POS.gameObject.SetActive(true);
                }
                else canvas_POS.gameObject.SetActive(false);
            }

            if (activeInteraction.GetComponent<Dustbin>())
            {
                // Handle interaction with Dustbin
                if (isHoldingItem)
                {
                    // If the player is holding an item, destroy it
                    Destroy(itemHolder.GetChild(0).gameObject); // Assuming itemHolder holds the item
                    isHoldingItem = false; // Update holding status
                    Debug.Log("Food item thrown in the dustbin!");
                }
                else
                {
                    Debug.Log("No food item to throw away.");
                }
            }
        }
    }

    #region Pickup Systems
    //Used to pick up an item into the ItemHolder
    void PickupItem(GameObject item)
    {
        //Debug.Log("Picking Up Item");

        item.transform.parent = itemHolder;
        item.transform.localPosition = Vector3.zero;
        isHoldingItem = true;

        interactionsInRange.Remove(item);
    }

    //Drop item on nearest avaliable spot
    void DropItem(Transform dropPosition)
    {
        Debug.Log("Dropping Item");

        GameObject item = itemHolder.GetChild(0).gameObject;

        item.transform.parent = null;
        item.transform.position = dropPosition.position;
        isHoldingItem = false;

        //InteractionsInRange.Add(item);

    }

    void DeliverOrder(Table currentTable)
    {
        #region Old Code
        //Debug.Log("Delivering Order to Table");

        //if (isHoldingItem)
        //{
        //    GameObject item = itemHolder.GetChild(0).gameObject;

        //    if (item.GetComponent<Interactable>())                  // Replace "Interactable"(super class) with "FoodItem"(sub class)
        //    {
        //        if (interactionsInRange[0].GetComponent<Table>())
        //        {
        //            Table table = interactionsInRange[0].GetComponent<Table>();

        //            for (int k = 0; k < table.customers.Count; k++)
        //            {
        //                if (!table.customers[k].HasGottenFood)
        //                {

        //                    if (table.customers[k].order.food == Food.Chips)           //Add  ->    "&& item.GetComponent<FoodItem>().food == Food.Chips"
        //                    {
        //                        Destroy(item);
        //                        table.customers[k].HasGottenFood = true;
        //                        isHoldingItem = false;
        //                        break;
        //                    }
        //                    else if (table.customers[k].order.food == Food.Burger)      //Add  ->    "&& item.GetComponent<FoodItem>().food == Food.Burger"
        //                    {
        //                        Destroy(item);
        //                        table.customers[k].HasGottenFood = true;
        //                        isHoldingItem = false;
        //                        break;
        //                    }
        //                    else if (table.customers[k].order.food == Food.Pizza)       //Add  ->    "&& item.GetComponent<FoodItem>().food == Food.Pizza"
        //                    {
        //                        Destroy(item);
        //                        table.customers[k].HasGottenFood = true;
        //                        isHoldingItem = false;
        //                        break;
        //                    }

        //                }
        //            }
        //        }
        //    }
        //}
        #endregion

        #region NewCode
        Debug.Log("Delivering Order to Table");

        if (isHoldingItem)
        {
            GameObject item = itemHolder.GetChild(0).gameObject; // Get the held item

            if (item.GetComponent<FoodItem>()) // Ensure the item is a FoodItem
            {
                FoodItem playerFoodItem = item.GetComponent<FoodItem>(); // Get the FoodItem component from the held item

                for (int k = 0; k < currentTable.customers.Count; k++)
                {
                    if (!currentTable.customers[k].HasGottenFood) // Check if the customer hasn't gotten their food
                    {
                        if (currentTable.customers[k].order.food == playerFoodItem.food) // Check if the customer's order matches the player's food item
                        {
                            Destroy(item); // Remove the food item from the player
                            currentTable.customers[k].HasGottenFood = true; // Mark the customer as having received their food
                            isHoldingItem = false; // Update holding status
                            Debug.Log("Correct food delivered to the customer!");
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning("Held item is not a FoodItem.");
            }
        }
        #endregion
    }
    #endregion

    void GetMostRelevantInteraction()
    {
        if (interactionsInRange.Count == 0)
        {
            activeInteraction = null;
            return;
        }

        activeInteraction = interactionsInRange[0];

        #region OldCode
        ////Searches through all available interactions and chooses one that is relevant to current position
        //if (isHoldingItem)
        //{
        //    for (int k = 0; k < interactionsInRange.Count; k++)
        //    {
        //        if (interactionsInRange[k].GetComponent<DropLocation>())
        //        {
        //            activeInteraction = interactionsInRange[k];
        //            break;
        //        }
        //        else if (interactionsInRange[k].GetComponent<Table>())
        //        {
        //            activeInteraction = interactionsInRange[k];
        //            break;
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    }
        //}
        //else if (!isHoldingItem)
        //{
        //    for (int k = 0; k < interactionsInRange.Count; k++)
        //    {
        //        if (interactionsInRange[k].GetComponent<FoodItem>())
        //        {
        //            activeInteraction = interactionsInRange[k];
        //            break;
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    }
        //}

        //if (interactionsInRange.Count <= 0)
        //    activeInteraction = null;

        //}
        #endregion
    }

    void ValidateInteractions()
    {
        // Clean up the 'interactionsInRange' list
        HashSet<GameObject> uniqueInteractions = new HashSet<GameObject>(); // Use a HashSet to store unique entries

        for (int i = interactionsInRange.Count - 1; i >= 0; i--) // Loop backwards for safe removal
        {
            if (interactionsInRange[i] == null || interactionsInRange[i].Equals(null) || !interactionsInRange[i].activeInHierarchy)
            {
                interactionsInRange.RemoveAt(i); // Remove missing, destroyed, or inactive objects
            }
            else if (!uniqueInteractions.Add(interactionsInRange[i])) // Attempt to add to HashSet, returns false if duplicate
            {
                interactionsInRange.RemoveAt(i); // Remove duplicate entries
            }
        }

        // Clean up the 'activeInteraction' reference
        if (activeInteraction == null || activeInteraction.Equals(null) || !activeInteraction.activeInHierarchy)
        {
            activeInteraction = null; // Reset to null if the reference is missing or inactive
        }
    }
}
