using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Vector3 currentCheckpointPosition;
    private Quaternion currentCheckpointRotation;
    private bool checkpointFacingRight;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Vector3 position, Quaternion rotation, bool facingRight)
    {
        currentCheckpointPosition = position;
        currentCheckpointRotation = rotation;
        checkpointFacingRight = facingRight;
        Debug.Log($"GameManager SetCheckpoint: {currentCheckpointPosition}, Facing right: {checkpointFacingRight} by {new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().DeclaringType.Name}");
    }

    public void RespawnAtCheckpoint()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();

            if (currentCheckpointPosition != null)
            {
                Debug.Log("GameManager RespawnAtCheckpoint: Respawning player at last checkpoint: " + currentCheckpointPosition);
                player.transform.position = currentCheckpointPosition;
                player.transform.rotation = currentCheckpointRotation;

                if (playerController != null)
                {
                    playerController.facingRight = checkpointFacingRight;
                }
                else
                {
                    Debug.LogWarning("PlayerController component not found on the player object.");
                }

                Debug.Log($"Player respawned at checkpoint: {currentCheckpointPosition}, Facing right: {checkpointFacingRight}");
            }
            else
            {
                Debug.Log("No checkpoint set.");
            }
        }
        else
        {
            Debug.LogError("Player not found in the scene.");
        }
    }
}
