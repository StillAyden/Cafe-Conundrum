using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float speed;

    private bool isInitialized = false;

    public void Initialize(Transform[] pathWaypoints, float moveSpeed)
    {
        waypoints = pathWaypoints;
        speed = moveSpeed;
        isInitialized = true;
    }

    private void Update()
    {
        if (!isInitialized || waypoints == null || waypoints.Length == 0)
            return;

        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        // Check if we reached the end of the path
        if (currentWaypointIndex >= waypoints.Length)
        {
            Destroy(gameObject); // Destroy customer
            return;
        }

        // Move towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;

        // Move customer
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);

        // Rotate customer to face the direction
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // Check if we have reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }
}

