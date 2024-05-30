using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GradientTriangle : MonoBehaviour
{
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
    }
}
