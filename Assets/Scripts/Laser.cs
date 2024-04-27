using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public PressurePLate pressurePlate;
    private bool isEnabled = true; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         Debug.Log(pressurePlate.isEnabled);
        if (!pressurePlate.isEnabled && isEnabled) {
            isEnabled = false;
            Vector3 newPosition = transform.position;
            newPosition.y = -100f;
            transform.position = newPosition;
        }
    }
}
