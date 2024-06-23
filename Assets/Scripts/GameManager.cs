using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Transform currentCheckpoint;
    private bool checkpointFacingRight;
    private Vector3 initialPlayerPosition;
    private bool initialPlayerFacingRight;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset the current checkpoint when a new scene is loaded
        currentCheckpoint = null;
        Debug.Log($"Scene loaded: {scene.name}. Checkpoint reset.");

        // Find the player and set the initial position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // debug player facingRight value
            Debug.Log("PlayerController Start: Player position is " + player.transform.position + ", Facing right: " + player.GetComponent<PlayerController>().facingRight);

            initialPlayerPosition = player.transform.position;
            initialPlayerFacingRight = true;
            Debug.Log($"Initial player position set to: {initialPlayerPosition}, Facing right: {initialPlayerFacingRight}");
        }
        else
        {
            Debug.LogWarning("Player not found in the scene on load.");
        }
    }

    public void SetCheckpoint(Transform checkpointTransform, bool facingRight)
    {
        currentCheckpoint = checkpointTransform;
        checkpointFacingRight = facingRight;
        Debug.Log($"Checkpoint set at: {currentCheckpoint.position}, Facing right: {checkpointFacingRight} by {new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().DeclaringType.Name}");
    }

    public void RespawnAtCheckpoint()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (currentCheckpoint != null)
            {
                player.transform.position = currentCheckpoint.position;
                player.transform.rotation = currentCheckpoint.rotation;
                playerController.facingRight = checkpointFacingRight;
                Debug.Log($"Player respawned at checkpoint: {currentCheckpoint.position}, Facing right: {checkpointFacingRight}");
            }
            else
            {
                player.transform.position = initialPlayerPosition;
                playerController.facingRight = initialPlayerFacingRight;
                Debug.Log($"No checkpoint set. Player respawned at initial position: {initialPlayerPosition}, Facing right: {initialPlayerFacingRight}");
            }
        }
        else
        {
            Debug.LogError("Player not found in the scene.");
        }
    }
}
