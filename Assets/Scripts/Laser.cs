using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public PressurePlate pressurePlate;
    private bool isEnabled = true; 

    // Update is called once per frame
    void Update()
    {
        if (pressurePlate && !pressurePlate.isEnabled && isEnabled) {
            isEnabled = false;
            Vector3 newPosition = transform.position;
            newPosition.y = -100f;
            transform.position = newPosition;
        }
    }
}
