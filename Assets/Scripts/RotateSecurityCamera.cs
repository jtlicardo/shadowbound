using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSecurityCamera : MonoBehaviour
{
    public float rotationAmount;

    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, (Mathf.Sin(Time.realtimeSinceStartup) * rotationAmount) + transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
