using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    public float height = 5f; // max height
    public float speed = 2f;
    public float pauseDuration = 2f; // duration of the pause at top and bottom

    private float originalY;
    private float targetY;
    private bool movingUp = true;
    private float pauseTimer;

    void Start()
    {
        originalY = transform.position.y;
        targetY = originalY + height;
    }

    void Update()
    {
        if (pauseTimer > 0)
        {
            pauseTimer -= Time.deltaTime;
            return;
        }

        float step = speed * Time.deltaTime;
        if (movingUp)
        {
            if (transform.position.y < targetY)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + step, transform.position.z);
            }
            else
            {
                pauseTimer = pauseDuration;
                movingUp = false;
            }
        }
        else
        {
            if (transform.position.y > originalY)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - step, transform.position.z);
            }
            else
            {
                pauseTimer = pauseDuration;
                movingUp = true;
            }
        }
    }
}
