using UnityEngine;
using System.Collections.Generic;

public class LightCulling : MonoBehaviour
{
    public Camera mainCamera;
    public float cullDistance = 50f;
    private Light[] allLights;
    private List<Light> visibleLights = new List<Light>();

    void Start()
    {
        // Get all lights in the scene
        allLights = FindObjectsOfType<Light>();
    }

    void Update()
    {
        CullLights();
    }

    void CullLights()
    {
        visibleLights.Clear();

        foreach (Light light in allLights)
        {
            // Check if light is within camera's view frustum and cull distance
            if (IsLightVisible(light))
            {
                visibleLights.Add(light);
                light.enabled = true;
            }
            else
            {
                light.enabled = false;
            }
        }
    }

    bool IsLightVisible(Light light)
    {
        // Check if light is within cull distance
        if (Vector3.Distance(mainCamera.transform.position, light.transform.position) > cullDistance)
            return false;

        // Check if light is within camera's view frustum
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        return GeometryUtility.TestPlanesAABB(frustumPlanes, new Bounds(light.transform.position, Vector3.one * light.range));
    }
}