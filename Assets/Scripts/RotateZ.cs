using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateZ : MonoBehaviour
{
    public float speed = 10.0f;

    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
