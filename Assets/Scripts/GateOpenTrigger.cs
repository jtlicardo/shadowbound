using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpenTrigger : MonoBehaviour
{
    public Gate gate; // Reference to the gate
    public PressurePlate pressurePlate; // This pressure plate needs to be activated to open the gate

    private bool canBeOpened = false;

    void Update()
    {
        if (pressurePlate && !pressurePlate.isEnabled && !canBeOpened)
        {
            canBeOpened = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (canBeOpened && gate != null && other.tag == "Player")
        {
            gate.OpenGate();
            Debug.Log("Player entered the gate trigger. Opening gate.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (gate != null && other.tag == "Player")
        {
            gate.CloseGate();
            Debug.Log("Player exited the gate trigger. Closing gate.");
        }
    }
}
