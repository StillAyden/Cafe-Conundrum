using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    #region Variables
    [Header("Table Settings")]
    [SerializeField] private List<GameObject> chairs = new List<GameObject>(); // List of chairs of this table
    [SerializeField] private int maxCapacity = 4; // Maximum number of people the table can hold
    [SerializeField] private bool isOccupied = false; // Showes if the table is taken
    [SerializeField] private int currentCustomers = 0; // Showes the current amount of customers
    #endregion

    #region Functions

    public bool IsFull() // Function to check if the table is full
    {
        if (isOccupied && currentCustomers >= 1)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }
 
    public void OccupyTable(int numberOfCustomers) // Function to occupy the table
    {
        isOccupied = true;
        currentCustomers = numberOfCustomers;
    }

    public void FreeTable() // Function to free the table
    {
        isOccupied = false;
        currentCustomers = 0;
    }

    public void AddCustomer(GameObject customer)
    {
        if (currentCustomers < maxCapacity)
        {
            // Assign customer to a chair (this assumes chairs are already set up in the inspector)
            GameObject chair = chairs[currentCustomers];
            customer.transform.position = chair.transform.position; // Place customer at the chair position
            currentCustomers++;
        }
    }

    #endregion
}
