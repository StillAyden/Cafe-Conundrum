using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    #region Variables
    [Header("Table Settings")]
    [SerializeField] private List<GameObject> chairs = new List<GameObject>(); // List of chairs of this table
    [SerializeField] private int maxCapacity = 4; // Maximum number of people the table can hold
    [SerializeField] private int currentCustomers = 0; // Showes the current amount of customers
    [SerializeField] private List<Customer> customers = new List<Customer>(); // List of customers at the table

    #endregion

    private void Start()
    {
        InitializeChairs();
    }

    private void OnValidate()
    {
        InitializeChairs();
    }

    #region Functions

    public void InitializeChairs() //Add chairs auto to make easier
    {
        chairs.Clear(); 
        foreach (Transform child in transform)
        {
            chairs.Add(child.gameObject);
        }
        maxCapacity = chairs.Count;
    }

    public bool IsFull() // Function to check if the table is full
    {
        return currentCustomers >= 1 ? true: false;
    }

    public bool ClearTable()
    {
        // Check if all customers are done eating
        foreach (Customer cus in customers)
        {
            if (cus != null && !cus.GetIsDoneEating())
            {
                return false; // Return false if any customer is not done eating
            }
        }

        // If all customers are done eating
        foreach (Customer cus in customers)
        {
            if (cus != null)
            {
                Destroy(cus.gameObject); // Destroy the customer GameObject
            }
        }

        // Clear the list of customers & Reset the customer count
        customers.Clear(); 
        currentCustomers = 0; 

        return true; // Return true if the table was cleared
    }

    public void ForceClearTable()
    {
        foreach (Customer cus in customers)
        {
            if (cus != null)
            {
                Destroy(cus.gameObject); // Destroy the customer GameObject
            }
        }

        // Clear the list of customers & Reset the customer count
        customers.Clear(); 
        currentCustomers = 0; 
    }

    public void AddCustomers(GameObject customerPrefab, int numberOfCustomers)
    {
        for (int i = 0; i < numberOfCustomers && currentCustomers < maxCapacity; i++)
        {
            //Get chair
            GameObject chair = chairs[currentCustomers];

            //Spawn customer at chair
            GameObject obj = Instantiate(customerPrefab, chair.transform.position,Quaternion.identity);

            //Add customer to customers List
            Customer cus = obj.GetComponent<Customer>();
            customers.Add(cus);

            //Increase num
            currentCustomers++;
        }
    }

    #endregion

    #region GetSte

    public int GetMaxCapacity(){ return maxCapacity; }

    #endregion
}
