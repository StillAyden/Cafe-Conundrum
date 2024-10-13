using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    #region Variables
    public static Kitchen Instance;

    //List to store all orders
    [HideInInspector] public List<List<Food>> kitchenOrders = new List<List<Food>>();
    [SerializeField][Range(1,6)] public float foodPrepTime = 3;
    private bool isProcessingOrders = false;

    //Food stuff
    [SerializeField] Transform foodSpawnLocation;
    [SerializeField] private GameObject foodPrefab;

    //Food Data
    [SerializeField] private foodData food;

    #endregion

    #region Private Methods

    private void Awake()
    {
        Instance = this;
    }
    private IEnumerator ProcessOrders()
    {
        isProcessingOrders = true;

        while (kitchenOrders.Count > 0)
        {
            Debug.Log("Next List");
            List<Food> currentOrderList = new List<Food>(kitchenOrders[0]);

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

    private void SpawnFood(Food order)
    {
        // Find the corresponding prefab in foodData.items based on the food type
        GameObject foodPrefab = null;

        foreach (foodItems foodItem in food.items)
        {
            if (foodItem.type == order)
            {
                foodPrefab = foodItem.prefab; // Assuming you have a prefab field in each food item
                break;
            }
        }

        // Ensure that the foodPrefab is found before instantiating
        if (foodPrefab != null)
        {
            GameObject spawnedFood = Instantiate(foodPrefab, foodSpawnLocation.position, Quaternion.identity);
            Debug.Log("Food spawned: " + order.ToString());
        }
        else
        {
            Debug.LogWarning("No matching prefab found for food type: " + order.ToString());
        }
    }

    #endregion

    #region GetSet

    public void AddOrder(List<Food> orders)
    {
        kitchenOrders.Add(new List<Food>(orders));

        if (!isProcessingOrders)
        {
            StartCoroutine(ProcessOrders());
        }
    }

    #endregion
}
