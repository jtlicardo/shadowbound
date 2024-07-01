using System.Collections;
using UnityEngine;

public class SmallGate : MonoBehaviour
{
    public float riseHeight = 2.5f;
    public float riseSpeed = 1f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isRising = false;
    public DisappearingEnemy[] disappearingEnemies; // Array of DisappearingEnemy scripts

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * riseHeight;
    }

    public void RiseGate()
    {
        if (!isRising)
        {
            StartCoroutine(RiseGateCoroutine());
        }
    }

    private IEnumerator RiseGateCoroutine()
    {
        isRising = true;
        float journeyLength = Vector3.Distance(initialPosition, targetPosition);
        float startTime = Time.time;

        while (transform.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * riseSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, fractionOfJourney);
            yield return null;
        }

        isRising = false;

        // Disable the BoxCollider on the parent object
        Transform parentTransform = transform.parent;
        if (parentTransform != null)
        {
            BoxCollider parentCollider = parentTransform.GetComponent<BoxCollider>();
            if (parentCollider != null)
            {
                parentCollider.enabled = false;
            }
        }

        // Trigger the disappearing enemies
        foreach (DisappearingEnemy enemy in disappearingEnemies)
        {
            if (enemy != null)
            {
                enemy.StartWalkAndDisappear();
            }
        }
    }
}
