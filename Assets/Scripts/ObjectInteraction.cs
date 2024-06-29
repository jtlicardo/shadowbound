using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public GameObject panel;
    private bool hasTriggered = false;

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
        }
    }
}
