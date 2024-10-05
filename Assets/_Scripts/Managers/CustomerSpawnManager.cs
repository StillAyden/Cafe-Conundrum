using System.Collections;
using UnityEngine;

public class CustomerSpawnManager : MonoBehaviour
{
    #region Variables
    private TableManager tableManager; 

    [Header("Spawn Settings")]
    [SerializeField][Range(1,15)] private float minSpawnInterval = 1f;
    [SerializeField][Range(15, 60)] private float maxSpawnInterval = 5f;
    [SerializeField] private int totalCustomers = 10; // Total number of customers to spawn

    [Space]
    [Header("Customer Prefab")]
    [SerializeField] private GameObject customerPrefab;

    private float spawnTimer = 0f;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        //Find Manager
        tableManager = FindObjectOfType<TableManager>();

        //Start Timer
        ResetSpawnTimer();
    }

    private void FixedUpdate()
    {
        if (totalCustomers > 0)
        {
            spawnTimer -= Time.fixedDeltaTime;
            if (spawnTimer <= 0f)
            {
                StartCoroutine(SpawnCustomer());
                ResetSpawnTimer();
            }
            
        }
    }
    #endregion

    #region Functions

    private void ResetSpawnTimer()
    {
        spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
       // Debug.Log("Timer: "+spawnTimer);
    }

    private IEnumerator SpawnCustomer()
    {
        // Find an available table
        Table availableTable = tableManager.FindAvailableTable();
  
        if (availableTable != null && !availableTable.IsFull())
        {
            // Determine the number of customers to spawn (1-4)
            int numberOfCustomers = Random.Range(1, availableTable.GetMaxCapacity()+1);
            Debug.Log("Customers: " + numberOfCustomers);

            // Spawn the customers and add to the table
            availableTable.AddCustomers(customerPrefab,numberOfCustomers);

            totalCustomers--;
        }
        yield return null;
    }


    #endregion
}
