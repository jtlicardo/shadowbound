using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DisappearingEnemy : MonoBehaviour
{
    public Vector3 walkDirection = Vector3.forward;
    public float walkDistance = 10f;
    public float walkSpeed = 2f;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool isWalking = false;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + walkDirection.normalized * walkDistance;
    }

    public void StartWalkAndDisappear()
    {
        if (!isWalking)
        {
            StartCoroutine(WalkAndDisappearCoroutine());
        }
    }

    private IEnumerator WalkAndDisappearCoroutine()
    {
        isWalking = true;
        float journeyLength = Vector3.Distance(startPosition, endPosition);
        float startTime = Time.time;

        float distanceCovered = 0;

        while (distanceCovered <= walkDistance)
        {
            distanceCovered = (Time.time - startTime) * walkSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            yield return null;
        }

        // Disable the enemy GameObject
        gameObject.SetActive(false);
        isWalking = false;
    }

    // For debugging purposes
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + walkDirection.normalized * walkDistance);
    }
}
