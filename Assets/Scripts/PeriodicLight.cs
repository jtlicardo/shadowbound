using UnityEngine;

public class PeriodicLight : MonoBehaviour
{
    public float startDelay = 0;
    private Vector3 originalPosition;
    private Vector3 lowerPosition;
    private bool movingToLowerPosition = true;
    private float changeInterval = 3.0f;
    private float timeSinceLastChange = 0.0f;
    private float elapsedTime = 0.0f;
    private bool startedChangingPosition = false;

    void Start()
    {

        originalPosition = transform.position;


        lowerPosition = new Vector3(originalPosition.x, originalPosition.y - 100, originalPosition.z);
    }

    void Update()
    {

        elapsedTime += Time.deltaTime;


        if (!startedChangingPosition && elapsedTime >= startDelay)
        {
            startedChangingPosition = true;
            timeSinceLastChange = 0.0f; 
        }

    
        if (!startedChangingPosition) return;

   
        timeSinceLastChange += Time.deltaTime;

        
        if (timeSinceLastChange >= changeInterval)
        {
        
            timeSinceLastChange = 0.0f;

         
            if (movingToLowerPosition)
            {
                transform.position = lowerPosition;
            }
            else
            {
                transform.position = originalPosition;
            }

       
            movingToLowerPosition = !movingToLowerPosition;
        }
    }
}
