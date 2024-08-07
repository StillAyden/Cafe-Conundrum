using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawner : MonoBehaviour
{
    #region Variables
    private TableManager tableManager; 

    [Header("Spawn Settings")]
    [SerializeField] private float minSpawnInterval = 1f;
    [SerializeField] private float maxSpawnInterval = 5f;
    [SerializeField] private GameObject customerPrefab;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        tableManager = FindObjectOfType<TableManager>();

        StartCoroutine(SpawnCustomerRoutine());
    }

    #region Functions

    // Coroutine to spawn customers at random intervals
    private IEnumerator SpawnCustomerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));

            // Find an available table
            Table availableTable = tableManager.FindAvailableTable();

            if (availableTable != null && !availableTable.IsFull())
            {
                // Determine the number of customers to spawn (1-4)
                int numberOfCustomers = Random.Range(1, 5);

                availableTable.OccupyTable(numberOfCustomers);

                for (int i = 0; i < numberOfCustomers; i++)
                {
                    // Spawn a customer and add to the table
                    GameObject customer = Instantiate(customerPrefab);
                    availableTable.AddCustomer(customer);
                }
            }
        }
    }


    #endregion
}
