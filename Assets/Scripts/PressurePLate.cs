using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PressurePLate : MonoBehaviour
{
    public Transform checkpoint;
    public PlayerController player;
    public bool isEnabled = true;
    public GameObject Box;
    Collider[] boxCollisions;
    float boxCheckRadius = 0.5f;
    public LayerMask boxLayer;

    void Update()
    {
        if (isEnabled) {
            boxCollisions = Physics.OverlapSphere(transform.position, boxCheckRadius, boxLayer);

            if (boxCollisions.Length > 0) {
                isEnabled = false;
                player.setCheckpoint(checkpoint.position);
            } 
            else isEnabled = true;
        }
    }
    void Start()
    {

    }
}
