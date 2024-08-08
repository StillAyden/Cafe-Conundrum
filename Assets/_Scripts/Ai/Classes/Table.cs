using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    #region Variables
    [Header("Table Settings")]
    [SerializeField] private List<GameObject> chairs = new List<GameObject>(); // List of chairs of this table
    [SerializeField] private int maxCapacity = 4; // Maximum number of people the table can hold
    [SerializeField] private int currentCustomers = 0; // Showes the current amount of customers

    //Button 
    [SerializeField] private bool CalculateChairs = false;
    #endregion

    private void Start()
    {
        InitializeChairs();
    }

    private void OnValidate()
    {
        InitializeChairs();
        CalculateChairs = false;
    }

    #region Functions

    private void InitializeChairs() //Add chairs auto to make easier
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

    public void FreeTable() // Function to free the table
    {
        currentCustomers = 0;
    }

    public void AddCustomers(GameObject customerPrefab, int numberOfCustomers)
    {
        for (int i = 0; i < numberOfCustomers && currentCustomers < maxCapacity; i++)
        {
            //Get chair
            GameObject chair = chairs[currentCustomers];

            //Spawn at chair
            Instantiate(customerPrefab, chair.transform.position,Quaternion.identity);

            //Increase num
            currentCustomers++;
        }
    }

    #endregion

    #region GetSte

    public int GetMaxCapacity(){ return maxCapacity; }

    #endregion
}
