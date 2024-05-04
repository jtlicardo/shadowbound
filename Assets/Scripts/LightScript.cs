using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public GameObject light;
    public PressurePLate pressurePlate;
    private bool isEnabled = true; 
    // Start is called before the first frame update
    void Start()
    {
        light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pressurePlate.isEnabled && isEnabled) {
            isEnabled = false;
            light.SetActive(true);
        }
    }
}
