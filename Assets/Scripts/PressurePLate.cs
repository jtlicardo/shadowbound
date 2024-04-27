using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PressurePLate : MonoBehaviour
{
    Rigidbody myRB;
    public bool isEnabled = true;
    public GameObject Box;
    Collider[] boxCollisions;
    float boxCheckRadius = 0.5f;
    public LayerMask boxLayer;

    void Update()
    {
        boxCollisions = Physics.OverlapSphere(myRB.position, boxCheckRadius, boxLayer);

        if (boxCollisions.Length > 0) isEnabled = false;
        else isEnabled = true;
    }
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }
}
