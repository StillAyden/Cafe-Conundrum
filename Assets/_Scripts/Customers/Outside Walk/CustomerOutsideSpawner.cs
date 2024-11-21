using System.Collections;
using UnityEngine;

public class CustomerOutsideSpawner : MonoBehaviour
{
    [Header("Path Settings")]
    public Transform[] path1Waypoints;
    public Transform[] path2Waypoints;

    [Header("Customer Settings")]
    public GameObject customerPrefab;
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 5f;

    [Header("Customer Speed")]
    public float minCustomerSpeed = 1f;
    public float maxCustomerSpeed = 3f;

    private void Start()
    {
        StartCoroutine(SpawnCustomers());
    }

    IEnumerator SpawnCustomers()
    {
        while (true)
        {
            // Wait for a random interval
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);

            // Randomly choose a path (0 or 1)
            int pathChoice = Random.Range(0, 2);

            if (pathChoice == 0)
            {
                SpawnCustomer(path1Waypoints);
            }
            else
            {
                SpawnCustomer(path2Waypoints);
            }
        }
    }

    void SpawnCustomer(Transform[] pathWaypoints)
    {
        if (pathWaypoints.Length == 0) return;

        // Instantiate customer at the first waypoint
        GameObject customer = Instantiate(customerPrefab, pathWaypoints[0].position, Quaternion.identity);

        // Assign random speed
        float randomSpeed = Random.Range(minCustomerSpeed, maxCustomerSpeed);

        // Get the CustomerMovement script and set the path and speed
        CustomerMovement movement = customer.GetComponent<CustomerMovement>();
        movement.Initialize(pathWaypoints, randomSpeed);

        movement.gameObject.transform.parent = this.transform;
    }

    // Draw lines between waypoints in the editor
    private void OnDrawGizmos()
    {
        DrawPathGizmos(path1Waypoints, Color.green);
        DrawPathGizmos(path2Waypoints, Color.blue);
    }

    void DrawPathGizmos(Transform[] waypoints, Color color)
    {
        if (waypoints == null || waypoints.Length < 2) return;

        Gizmos.color = color;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            if (waypoints[i] != null && waypoints[i + 1] != null)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }
}
