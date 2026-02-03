using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    // Directional Light Object Source
    [SerializeField]
    private GameObject DirectionalLight;

    private Vector3 lightDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Light shines out towards the scene
        lightDir = -DirectionalLight.transform.forward; // Forward Axis points away from the object but the light source should point towards the object
    }

    // Update is called once per frame
    void Update()
    {
        // Per-Object shading
        //if (SurfaceData.MeshRenderer != null && SurfaceData.MeshRenderer.material != null)
        //{
        //    SurfaceData.MeshRenderer.material.color = LambertDiffuse_M(SurfaceData.avgNormal, lightDir);
        //}

        // Vertex Shading (Vertex shading unlit is not supported currently)
        SurfaceData._mesh.colors = LambertDiffuse_V(SurfaceData.normals, lightDir);

        Debug.DrawRay(transform.position, lightDir.normalized * 2, Color.yellow);
        DrawNormals(SurfaceData.normals);
    }

    Color[] LambertDiffuse_V(Vector3[] normals, Vector3 lightDir)
    {
        Color[] albedos = new Color[normals.Length];

        for(int i = 0; i < normals.Length; i++)
        {
            Vector3 worldNormal = transform.TransformDirection(normals[i]);
            float intensity = Mathf.Max(0, Vector3.Dot(worldNormal, lightDir));
            albedos[i] = SurfaceData.albedo * intensity;
        }

        return albedos;
    }

    Color LambertDiffuse_M(Vector3 avgNormal, Vector3 lightDir)
    {
        Vector3 avgNormal_ = transform.TransformDirection(avgNormal);
        float intensity = Mathf.Max(0, Vector3.Dot(avgNormal_, lightDir));
        return SurfaceData.albedo * intensity;
    }

    void DrawNormals(Vector3[] normals)
    {
        foreach(Vector3 n in normals)
        {
            Debug.DrawRay(transform.position, n * 2, Color.red);
        }
    }
}
