using UnityEngine;

public class EnemyAITest : MonoBehaviour
{
    public Transform[] waypoints; 
    public float speed = 2.0f;
    private int currentWaypointIndex = 0;

    void Update()
    {
        Transform wp = waypoints[currentWaypointIndex];

        if (Vector3.Distance(transform.position, wp.position) < 0.1f) {
            if (currentWaypointIndex == 0) currentWaypointIndex = 1;
            else currentWaypointIndex = 0;
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, wp.position, speed * Time.deltaTime);
            transform.LookAt(wp.position);
        }
    }
}
