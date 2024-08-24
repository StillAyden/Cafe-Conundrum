/* PlayerController.cs
 * 
 *  This script controls all of the movement and interaction from the player
 */

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    #region OnTrigger Events
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Interactable>())
        {
            interactionsInRange.Add(col.gameObject);
            activeInteraction = interactionsInRange[0];
        }
        
        GetMostRelevantInteraction();
    }

    private void OnTriggerExit(Collider col)
    {
        if (interactionsInRange.Contains(col.gameObject))
        {
            interactionsInRange.Remove(col.gameObject);
        }

        if (interactionsInRange.Count > 0)
        {
            activeInteraction = interactionsInRange[0];
        }
        
        GetMostRelevantInteraction();
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

            if (activeInteraction.GetComponent<Table>() && itemHolder.GetChild(0)?.GetComponent<FoodItem>())
            {
                //If has food item -> give to table
                DeliverOrder(activeInteraction.GetComponent<Table>());
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
            
            //if (activeInteraction.GetComponent<Interactable>() != null)
            //{
            //    //throw new NotImplementedException();
            //}
        }
    }

    #region Pickup Systems
    //Used to pick up an item into the ItemHolder
    void PickupItem(GameObject item)
    {
        Debug.Log("Picking Up Item");

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
        Debug.Log("Delivering Order to Table");

        if (isHoldingItem == true)
        {
            GameObject item = itemHolder.GetChild(0).gameObject;
            
            if (item.GetComponent<Interactable>())                  // Replace "Interactable"(super class) with "FoodItem"(sub class)
            {
                if (interactionsInRange[0].GetComponent<Table>())
                {
                    Table table = interactionsInRange[0].GetComponent<Table>();

                    for (int k = 0; k < table.customers.Count; k++)
                    {
                        if (!table.customers[k].HasGottenFood)
                        {

                            if (table.customers[k].order.food == Food.Chips)           //Add  ->    "&& item.GetComponent<FoodItem>().food == Food.Chips"
                            {
                                Destroy(item);
                                table.customers[k].HasGottenFood = true;
                                isHoldingItem = false;
                                break;
                            }
                            else if (table.customers[k].order.food == Food.Burger)      //Add  ->    "&& item.GetComponent<FoodItem>().food == Food.Burger"
                            {
                                Destroy(item);
                                table.customers[k].HasGottenFood = true;
                                isHoldingItem = false;
                                break;
                            }
                            else if (table.customers[k].order.food == Food.Pizza)       //Add  ->    "&& item.GetComponent<FoodItem>().food == Food.Pizza"
                            {
                                Destroy(item);
                                table.customers[k].HasGottenFood = true;
                                isHoldingItem = false;
                                break;
                            }

                        }
                    }
                }
            }
        }
    }
    #endregion


    void GetMostRelevantInteraction()
    {
        //Searches through all available interactions and chooses one that is relevant to current position
        if (isHoldingItem)
        {
            for (int k = 0; k < interactionsInRange.Count; k++)
            {
                if (interactionsInRange[k].GetComponent<DropLocation>())
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
                    continue;
                }
            }
        }
        else if (!isHoldingItem)
        {
            for (int k = 0; k < interactionsInRange.Count; k++)
            {
                if (interactionsInRange[k].GetComponent<FoodItem>())
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

        if (interactionsInRange.Count <= 0)
            activeInteraction = null;

    }
}
