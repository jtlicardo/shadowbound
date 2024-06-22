using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateHugeRotatingShit : MonoBehaviour
{
    public GameObject[] shadowCubes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject cube in shadowCubes)
            {
                cube.SetActive(true);
            }
        }
    }
}
