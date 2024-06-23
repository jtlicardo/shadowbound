using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;

    public float openSpeed = 1f;
    public float openDistance = 2f;

    private bool isOpening = false;
    private bool isClosing = false;

    private Vector3 leftDoorClosedPosition;
    private Vector3 rightDoorClosedPosition;

    private BoxCollider gateCollider;

    void Start()
    {
        if (leftDoor == null || rightDoor == null)
        {
            Debug.LogError("Gate: Left door or right door is not assigned!");
            return;
        }

        // Store the initial positions of the doors
        leftDoorClosedPosition = leftDoor.transform.localPosition;
        rightDoorClosedPosition = rightDoor.transform.localPosition;

        gateCollider = GetComponent<BoxCollider>();
        if (gateCollider == null)
        {
            Debug.LogError("Gate: No BoxCollider found on the gate object!");
        }
    }

    void Update()
    {
        if (isOpening)
        {
            OpenDoors();
        }
        else if (isClosing)
        {
            CloseDoors();
        }
    }

    public void OpenGate()
    {
        isOpening = true;
        isClosing = false;

        if (gateCollider != null)
        {
            gateCollider.enabled = false;
        }
    }

    public void CloseGate()
    {
        isClosing = true;
        isOpening = false;

        if (gateCollider != null)
        {
            gateCollider.enabled = true;
        }
    }

    void OpenDoors()
    {
        bool leftDoorOpen = MoveDoor(leftDoor, leftDoorClosedPosition + Vector3.forward * openDistance);
        bool rightDoorOpen = MoveDoor(rightDoor, rightDoorClosedPosition + Vector3.back * openDistance);

        if (leftDoorOpen && rightDoorOpen)
        {
            isOpening = false;
        }
    }

    void CloseDoors()
    {
        bool leftDoorClosed = MoveDoor(leftDoor, leftDoorClosedPosition);
        bool rightDoorClosed = MoveDoor(rightDoor, rightDoorClosedPosition);

        if (leftDoorClosed && rightDoorClosed)
        {
            isClosing = false;
        }
    }

    bool MoveDoor(GameObject door, Vector3 targetPosition)
    {
        door.transform.localPosition = Vector3.MoveTowards(
            door.transform.localPosition,
            targetPosition,
            openSpeed * Time.deltaTime
        );

        return Vector3.Distance(door.transform.localPosition, targetPosition) < 0.01f;
    }
}
