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
    private bool moving = true;
    private float pauseTimer;

    private BoxCollider boxCollider;

    public PressurePlate pressurePlate;

    void Start()
    {
        originalY = transform.position.y;
        targetY = originalY + height;
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (pauseTimer > 0)
        {
            pauseTimer -= Time.deltaTime;
            return;
        }

        if (pressurePlate && pressurePlate.isEnabled)
        {
            return;
        }

        float step = speed * Time.deltaTime;
        if (moving)
        {
            if (transform.position.y < targetY && height > 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + step, transform.position.z);
            }
            else if (transform.position.y > targetY && height < 0 && !IsPlayerBelow())
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - step, transform.position.z);
            }
            else
            {
                pauseTimer = pauseDuration;
                moving = false;
            }
        }
        else
        {
            if (transform.position.y > originalY && height > 0 && !IsPlayerBelow())
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - step, transform.position.z);
            }
            else if (transform.position.y < originalY && height < 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + step, transform.position.z);
            }
            else
            {
                pauseTimer = pauseDuration;
                moving = true;
            }
        }
    }

    bool IsPlayerBelow()
    {
        Bounds bounds = boxCollider.bounds;
        Vector3 origin = new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
        Vector3 halfExtents = new Vector3(bounds.extents.x, 0.1f, bounds.extents.z);
        RaycastHit hit;

        bool isHit = Physics.BoxCast(origin, halfExtents, Vector3.down, out hit, Quaternion.identity, 0.1f);
        if (isHit && hit.collider.CompareTag("Player"))
        {
            return true;
        }
        return false;
    }
}
