using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChild : MonoBehaviour
{
    private HorizontallyMovingPlatform parentScript;

    void Start()
    {
        parentScript = GetComponentInParent<HorizontallyMovingPlatform>();

        if (parentScript == null)
        {
            Debug.LogError("Parent script not found!");
        }
    }

    // these functions call the parent script's functions when the player enters/exits the trigger
    // parent script: HorizontallyMovingPlatform.cs
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parentScript.HandleEnter(other.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parentScript.HandleExit(other.transform);
        }
    }
}
