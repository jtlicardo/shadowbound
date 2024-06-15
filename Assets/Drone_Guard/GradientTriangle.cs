using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GradientTriangle : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = new Vector3[3]
        {
            new Vector3(0, 1, 0),   
            new Vector3(-1, -1, 0), 
            new Vector3(1, -1, 0)  
        };

        int[] triangles = new int[3]
        {
            0, 1, 2
        };

        Color[] colors = new Color[3]
        {
            new Color(1, 0, 0, 0.2f), 
            new Color(1, 0, 0, 0), 
            new Color(1, 0, 0, 0)  
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();

        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        // This is only needed for the security camera for now
        if (boxCollider != null)
        {
            // Check for overlaps with the darkness layer
            Vector3 boxSize = boxCollider.size * 0.9f;
            Collider[] hitColliders = Physics.OverlapBox(boxCollider.bounds.center, boxSize / 2, boxCollider.transform.rotation, LayerMask.GetMask("Darkness"));

            if (hitColliders.Length > 0)
            {
                meshRenderer.enabled = false; // Hide the triangle
            }
            else if (hitColliders.Length == 0)
            {
                meshRenderer.enabled = true; // Show the triangle
            }
        }
    }
}
