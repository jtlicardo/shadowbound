using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;

        transform.position = Vector3.Lerp(new Vector3(transform.position.x, (float)(transform.position.y + 0.3), transform.position.z), targetCamPos, smoothing * Time.deltaTime);
    }
}
