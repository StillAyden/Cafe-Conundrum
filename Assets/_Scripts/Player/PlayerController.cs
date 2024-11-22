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
    [SerializeField] bool enableSmoothMove;
    [SerializeField] public float moveSpeed = 200f;
    Vector3 moveDirection = Vector3.zero;
    [SerializeField] float rotateSpeed = 1f;

    [Header("Interactions")]
    [SerializeField] List<GameObject> interactionsInRange = new List<GameObject>();
    [SerializeField] GameObject activeInteraction;

    [Header("Pick Up")]
    [SerializeField] bool isHoldingItem = false;
    [SerializeField] Transform itemHolder;

    //[Header("POS System")]
    //[SerializeField] Canvas canvas_POS;

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
        Rotate();
    }

    private void Update()
    {
        Move();
        ValidateInteractions();
    }

    #region OnTrigger Events
    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out Interactable interactable))
        {
            if (!interactionsInRange.Contains(interactable.gameObject))
            {
                interactionsInRange.Add(interactable.gameObject); // Add the new interaction to the list
                //Debug.Log($"Added {interactable.gameObject.name} to interactions list.");
            }

            GetMostRelevantInteraction(); // Update the most relevant interaction

            if(activeInteraction && activeInteraction.transform.GetChild(0)?.GetComponent<Canvas>())
                activeInteraction.transform.GetChild(0).GetComponent<Canvas>()?.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (activeInteraction && activeInteraction.transform.GetChild(0)?.GetComponent<Canvas>())
            activeInteraction.transform.GetChild(0).GetComponent<Canvas>().gameObject.SetActive(false);


        if (col.TryGetComponent(out Interactable interactable))
        {
            if (interactionsInRange.Contains(interactable.gameObject))
            {
                interactionsInRange.Remove(interactable.gameObject); // Remove the interaction from the list
                //Debug.Log($"Removed {interactable.gameObject.name} from interactions list.");
            }

            // Check and update activeInteraction if it is the one that was removed
            if (activeInteraction == interactable.gameObject)
            {
                activeInteraction = null; // Clear activeInteraction if it was the one removed
                GetMostRelevantInteraction(); // Update the most relevant interaction

                if (activeInteraction && activeInteraction.transform.GetChild(0)?.GetComponent<Canvas>())
                    activeInteraction.transform.GetChild(0).GetComponent<Canvas>()?.gameObject.SetActive(true);
            }
        }

        UIManager.Instance.HideOrderingInterface();

    }
    #endregion
    void Rotate()
    {

        if (moveDirection != Vector3.zero)
        {
            Quaternion rotateTo = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z), Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateTo, rotateSpeed * Time.fixedDeltaTime);
        }
    }
    void Move()
    {
        if (!enableSmoothMove)
        {
            moveDirection = new Vector3(InputManager.Instance.moveInput.x * moveSpeed * Time.fixedDeltaTime,
                                        rb.velocity.y,
                                            InputManager.Instance.moveInput.y * moveSpeed * Time.fixedDeltaTime);

            rb.velocity = moveDirection;

        }
        else if (enableSmoothMove)
        {
            if (rb.velocity.magnitude < moveSpeed/50)
            {
                moveDirection = new Vector3(InputManager.Instance.moveInput.x * 1000f * Time.deltaTime, 0,
                                            InputManager.Instance.moveInput.y * 1000f * Time.deltaTime);

                rb.AddForce(moveDirection, ForceMode.Force);
            }
        }
    }
    void Interact()
    {
        if (activeInteraction != null)
        {
            if (activeInteraction.GetComponent<DropLocation>())
            {
                SoundManager.PlaySound(SoundType.TRASH_CAN, SoundMode.VFX, this.transform.position);
                //TODO: Get a better/cleaner way to do this
                if (isHoldingItem && activeInteraction.GetComponent<DropLocation>().occupied == false)
                {
                    //Drop currently held item
                    DropItem(activeInteraction.GetComponent<DropLocation>().gameObject.transform, activeInteraction.GetComponent<DropLocation>().offset);
                    //activeInteraction.GetComponent<DropLocation>().occupied = true;
                }
            }

            if (activeInteraction.GetComponent<FoodItem>())
            {
                SoundManager.PlaySound(SoundType.PLATE_CUP_CLINKING, SoundMode.VFX, this.transform.position);
                //If item is food -> Pick up
                if (!isHoldingItem)
                {
                    PickupItem(activeInteraction);

                    if (TutorialEvents.Instance)
                        TutorialEvents.Instance.getOrderTrigger = true;
                }
            }

            if (activeInteraction.GetComponent<DrinkItem>())
            {
                //If item is drink -> Pick up
                if (!isHoldingItem)
                {
                    PickupItem(activeInteraction);
                }
            }

            if (activeInteraction.GetComponent<Table>())
            {
                Table table = activeInteraction.GetComponent<Table>();

                // Check if the player is holding an item and deliver it
                if (isHoldingItem)
                {
                    DeliverOrder(table);

                    if (TutorialEvents.Instance)
                        TutorialEvents.Instance.serveDrinkTrigger = true;

                    if (TutorialEvents.Instance)
                        TutorialEvents.Instance.deliverOrderTrigger = true;
                }
                else
                {
                    table.IsOrderTaken = true; // Take the order at the table if not holding any item
                    Debug.Log("Order taken at the table!");

                    if (TutorialEvents.Instance)
                        TutorialEvents.Instance.goToTableTrigger = true;
                }
            }

            if (activeInteraction.GetComponent<OrderingSystem>())
            {
                SoundManager.PlaySound(SoundType.CASH_REGISTER, SoundMode.VFX, this.transform.position);
                //Debug.Log("Interacting with POS system");
                if (UIManager.Instance.isOrderingInterfaceActive == false)
                {
                    UIManager.Instance.ShowOrderingInterface();

                }
                else
                {
                    UIManager.Instance.HideOrderingInterface();
                    
                    if (TutorialEvents.Instance)
                        TutorialEvents.Instance.goToPOSSystemTrigger = true;
                }
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

            if (activeInteraction.GetComponent<BaristaMachine>())
            {
                if (!isHoldingItem)
                {
                    //GetItem(GameManager.Instance.drink.items[(int)Drink.Coffee].prefab);
                    GetItem(activeInteraction.GetComponent<BaristaMachine>().Interact());
                    
                    if (TutorialEvents.Instance)
                        TutorialEvents.Instance.getCustomerDrinkTrigger = true;
                }
            }

            if (activeInteraction.GetComponent<MiniFridge>())
            {
                if (!isHoldingItem)
                {
                    GetItem(activeInteraction.GetComponent<MiniFridge>().Interact());

                    if (TutorialEvents.Instance)
                        TutorialEvents.Instance.getCustomerDrinkTrigger = true;
                }
            }

            if (activeInteraction.GetComponent<Telephone>())
            {
                if (UIManager.Instance.isUpgradesInterfaceActive == false)
                    UIManager.Instance.ShowUpgradesInterface();
                else UIManager.Instance.HideUpgradesInterface();
            }
        }
    }

    #region Pickup Systems
    GameObject GetItem(GameObject item)
    {
        //Used to get items out of MiniFridge, CoffeeMachine, etc

        if (item != null)
        {
            isHoldingItem = true;
            GameObject temp = Instantiate(item, itemHolder);
            temp.transform.localPosition = Vector3.zero;
            interactionsInRange.Remove(item);
            return temp;
        }
        else return null;
    }
    void PickupItem(GameObject item)
    {
        //Used to pick up an item into the ItemHolder
        //Debug.Log("Picking Up Item");

        item.transform.parent = itemHolder;
        item.transform.localPosition = Vector3.zero;
        isHoldingItem = true;

        interactionsInRange.Remove(item);
    }

    void DropItem(Transform dropPosition, Vector3 offset)
    {
        //Drop item on nearest available spot
        Debug.Log("Dropping Item");

        GameObject item = itemHolder.GetChild(0).gameObject;

        item.transform.parent = null;
        item.transform.position = dropPosition.position + offset;
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
        Debug.Log("Attempting to Deliver Order to Table");

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
                        if (currentTable.customers[k].foodOrder == playerFoodItem.food) // Check if the customer's order matches the player's food item
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
            else if (item.GetComponent<DrinkItem>())
            {
                DrinkItem playerDrinkItem = item.GetComponent<DrinkItem>();

                for (int k = 0; k < currentTable.customers.Count; k++)
                {
                    if (!currentTable.customers[k].HasGottenDrink) // Check if the customer hasn't gotten their food
                    {
                        if (currentTable.customers[k].drinkOrder == playerDrinkItem.drink) // Check if the customer's order matches the player's food item
                        {
                            Destroy(item); // Remove the food item from the player
                            currentTable.customers[k].HasGottenDrink = true; // Mark the customer as having received their food
                            isHoldingItem = false; // Update holding status
                            Debug.Log("Correct Drink delivered to the customer!");
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning("Held item is not a FoodItem or a DrinkItem");
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

        //activeInteraction = interactionsInRange[0];     //What if there are 2 interactions right next to each other (e.g. OrderingSystem and DropLocation)????

        #region OldCode
        ////Searches through all available interactions and chooses one that is relevant to current position
        if (isHoldingItem)
        {
            for (int k = 0; k < interactionsInRange.Count; k++)
            {
                //NB: Order is IMPORTANT... Higher up takes priority
                if (interactionsInRange[k].GetComponent<Dustbin>())
                {
                    activeInteraction = interactionsInRange[k];
                    break;
                }
                if (interactionsInRange[k].GetComponent<DropLocation>() && interactionsInRange[k].GetComponent<DropLocation>()?.occupied == false)
                {
                    activeInteraction = interactionsInRange[k];
                    break;
                }
                else if (interactionsInRange[k].GetComponent<Table>())
                {
                    activeInteraction = interactionsInRange[k];
                    break;
                }
                else
                {
                    activeInteraction = null;
                    continue;
                }
            }
        }
        else if (!isHoldingItem)
        {
            for (int k = 0; k < interactionsInRange.Count; k++)
            {
                //NB: Order is IMPORTANT... Higher up takes priority
                if (interactionsInRange[k].GetComponent<OrderingSystem>())
                {
                    activeInteraction = interactionsInRange[k];
                    break;
                }
                else if (interactionsInRange[k].GetComponent<Table>())
                {
                    activeInteraction = interactionsInRange[k];
                    break;
                }
                else if (interactionsInRange[k].GetComponent<FoodItem>())
                {
                    activeInteraction = interactionsInRange[k];
                    break;
                }
                else if (interactionsInRange[k].GetComponent<DrinkItem>())
                {
                    activeInteraction = interactionsInRange[k];
                    break;
                }
                else if (interactionsInRange[k].GetComponent<BaristaMachine>())
                {
                    activeInteraction = interactionsInRange[k];
                    break;
                }
                else if (interactionsInRange[k].GetComponent<MiniFridge>())
                {
                    activeInteraction = interactionsInRange[k];
                    break;
                }
                else if (interactionsInRange[k].GetComponent<Telephone>())
                {
                    activeInteraction = interactionsInRange[k];
                    break;
                }
                else if (interactionsInRange[k].GetComponent<Generator>())
                {
                    activeInteraction = interactionsInRange[k];
                    break;
                }
                else if (interactionsInRange[k].GetComponent<WaterDispenser>())
                {
                    activeInteraction = interactionsInRange[k];
                    break;
                }
                else
                {
                    continue;
                }
            }
        }


        

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
