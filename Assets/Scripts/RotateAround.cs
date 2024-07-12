using UnityEngine;
public class RotateAround : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.forward, 20 * Time.deltaTime);
    }
}