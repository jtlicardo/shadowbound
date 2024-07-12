using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool playerShouldFaceRight = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetCheckpoint(this.transform.position, this.transform.rotation, playerShouldFaceRight);
        }
    }
}
