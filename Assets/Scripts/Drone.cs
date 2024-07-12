using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour
{
    public Transform[] waypoints; 
    public float speed = 5f;
    private int currentWaypointIndex = 0; 

    void Update()
    {
        if (waypoints.Length == 0)
            return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 targetPosition = targetWaypoint.position;
        Vector3 movementDirection = (targetPosition - transform.position).normalized;
        float distanceToMove = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, distanceToMove);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
