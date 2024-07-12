using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public GameObject panel;
    public MovingFloor movingFloor; // Reference to the MovingFloor script (optional)
    private bool hasTriggered = false;
    public DialogueTrigger dialogueTrigger; // Reference to the DialogueTrigger script (optional)
    public BoxCollider boxCollider; // Reference to a BoxCollider that needs to be activated (optional)

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            panel.SetActive(true);
        }
    }

    private void Update()
    {
        if (panel.activeSelf && Input.GetKeyDown(KeyCode.X))
        {
            panel.SetActive(false);
            hasTriggered = true;

            // Activate the moving floor
            if (movingFloor != null)
            {
                movingFloor.Activate();
            }

            // Trigger the dialogue
            if (dialogueTrigger != null)
            {
                dialogueTrigger.TriggerDialogue();
            }

            // Activate the box collider
            if (boxCollider != null)
            {
                boxCollider.enabled = true;
            }
        }
    }
}
