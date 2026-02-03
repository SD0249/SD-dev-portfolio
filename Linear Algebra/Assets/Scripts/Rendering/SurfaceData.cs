using UnityEngine;

// Stores Surface Data of the Model in the scene,
// and allows usage (read value) to outside functionalities
public class SurfaceData : MonoBehaviour
{
    // ** Unity only serializes things on the scene state (scene ownership) --> [PER INSTANCE]
    // Position, Rotation, Scale / References between objects /
    // Per-object configuration / Values that belong to this instance of a component

    // ** NOT program states
    // Static fields - Global per type, and type exists once per AppDomain
    // --> There is exactly one copy of each static field per class, shared across ALL scenes, ALL game objects, ALL scripts.
    // Globals
    // Singletons
    // Cached data

    // In C#
    // Files don't matter at runtime / Namespaces don't create storage /
    // Assemblies compile into code / Types are what actually exist
    [SerializeField] // Can only serialize non-static, fields, of supported types
    private GameObject displayedModel;     // Global Scope(visible in THIS file) / Internal Linkage

    [SerializeField]
    private Renderer meshRenderer;         // To retrieve Albedo / Base color information

    // Possible thing to fix later
    // NOTE: This properties would survive better with functions, because this is shared over a screen.
    //       If functions were used, then each model could retain each data of theirs.
    public static Mesh _mesh { get; private set; }

    public static Vector3[] normals { get; private set; }

    public static Color albedo { get; private set; }

    public static Vector3 avgNormal { get; private set; }

    public static Renderer MeshRenderer { get; private set; }

    private void Start()
    {
        Mesh mesh = displayedModel.GetComponent<MeshFilter>().sharedMesh;
        normals = mesh.normals;

        _mesh = mesh;

        if(meshRenderer.material.color != null)
        {
            albedo = meshRenderer.material.color;
            Debug.Log("Albedo: " + albedo);
        }
    }

    public void AverageNormal()
    {
        Vector3 avgNormal = Vector3.zero;

        for(int i = 0; i < normals.Length; i++)
        {
            avgNormal += normals[i];
        }

        // avgNormal = avgNormal / normals.Length;  // Unnecessary because we don't care about the length

        SurfaceData.avgNormal = avgNormal.normalized;    
    }
}
