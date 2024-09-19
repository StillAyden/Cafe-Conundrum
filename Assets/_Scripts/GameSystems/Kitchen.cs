using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    #region Variables

    //List to store all orders
    [HideInInspector] public List<List<Order>> kitchenOrders = new List<List<Order>>();
    [SerializeField][Range(1,6)] private float foodPrepTime = 3;
    private bool isProcessingOrders = false;

    //Food stuff
    [SerializeField] Transform foodSpawnLocation;
    [SerializeField] private GameObject foodPrefab;

    #endregion

    #region Private Methods

    private IEnumerator ProcessOrders()
    {
        isProcessingOrders = true;

        while (kitchenOrders.Count > 0)
        {
            Debug.Log("Next List");
            List<Order> currentOrderList = new List<Order>(kitchenOrders[0]);

            for (int i = 0; i < currentOrderList.Count; i++)
            {
                Debug.Log(i.ToString()); 
                yield return new WaitForSecondsRealtime(foodPrepTime); //Wait for 3 seconds
                SpawnFood(currentOrderList[i]); //Call SpawnFood function
            }

            Debug.Log("List is done");
            kitchenOrders.RemoveAt(0); //Remove the processed list from the queue
        }

        Debug.Log("Kitchen is done");
        isProcessingOrders = false; //All orders have been processed
    }

    private void SpawnFood(Order order)
    {
        //Spawn the prefab
        GameObject spawnedFood = Instantiate(foodPrefab, foodSpawnLocation.position, Quaternion.identity);

        //Select which prefab to spawn based on the food type
        spawnedFood.GetComponent<FoodItem>().SetFoodType(order.food);

        Debug.Log("Food spawned");
    }

    #endregion

    #region GetSet

    public void AddOrder(List<Order> orders)
    {
        kitchenOrders.Add(new List<Order>(orders));

        if (!isProcessingOrders)
        {
            StartCoroutine(ProcessOrders());
        }
    }

    #endregion
}
