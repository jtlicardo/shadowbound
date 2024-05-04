using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    public float crouchOffset = 0.5f;

    private PlayerController player;
    private Vector3 offset;

    Vector3 TargetPosition => target.position + offset;

    void Start()
    {
        player = target.GetComponent<PlayerController>();
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        // Adjust camera position if the player is crouching
        Vector3 targetPos = player != null && player.IsCrouching ? TargetPosition - Vector3.up * crouchOffset : TargetPosition;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.fixedDeltaTime);
    }
}
