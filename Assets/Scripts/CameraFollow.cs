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
        CalculatePosition();
    }

    public void CalculatePosition()
    {
        player = target.GetComponent<PlayerController>();

        // Compute initial camera position
        Vector3 initialPosition = new Vector3(target.position.x, target.position.y + 2.5f, target.position.z - 10f);

        // Set the camera's position to the computed initial position
        transform.position = initialPosition;

        offset = initialPosition - target.position;
    }

    void FixedUpdate()
    {
        // Adjust camera position if the player is crouching
        Vector3 targetPos = player != null && player.IsCrouching ? TargetPosition - Vector3.up * crouchOffset : TargetPosition;
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.fixedDeltaTime);
    }
}
