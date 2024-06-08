using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontallyMovingPlatform : MonoBehaviour
{
    public float width = 5f; // max horizontal distance
    public float speed = 2f;
    public float pauseDuration = 2f; // duration of the pause at left and right

    private float originalX;
    private float targetX;
    private bool movingRight = true;
    private float pauseTimer;

    void Start()
    {
        originalX = transform.position.x;
        targetX = originalX + width;
    }

    void Update()
    {
        if (pauseTimer > 0)
        {
            pauseTimer -= Time.deltaTime;
            return;
        }

        float step = speed * Time.deltaTime;
        if (movingRight)
        {
            if (transform.position.x < targetX)
            {
                transform.position = new Vector3(transform.position.x + step, transform.position.y, transform.position.z);
            }
            else
            {
                pauseTimer = pauseDuration;
                movingRight = false;
            }
        }
        else
        {
            if (transform.position.x > originalX)
            {
                transform.position = new Vector3(transform.position.x - step, transform.position.y, transform.position.z);
            }
            else
            {
                if (transform.position.x <= originalX)
                {
                    pauseTimer = pauseDuration;
                    movingRight = true;
                }
            }
        }
    }
}
