using UnityEngine;

// This wasn't really needed, and this also probably has to be in scriptable object, or a helper function of lighting.s
public class VectorOperation : MonoBehaviour
{
    // Returns the dot product between two vectors - indicates how much aligned they are
    public float DotProduct(Vector3 a, Vector3 b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;

        // return Vector3.Dot(a, b);        // If data doesn't respond well to floating point errors, resort here.
    }

    // Since dividing can cause issues, the Unity version is wrapped up here,
    // but the main operation under here is vector divided by its magnitude
    public Vector3 Normalize(Vector3 a)
    {
        return a.normalized;
    }

    // Returns the angle between to vectors (This might come in handy to show angle relation between vectors)
    public float Angle(Vector3 a, Vector3 b)
    {
        return Mathf.Acos(DotProduct(a, b) / (a.magnitude * b.magnitude));
    }
}

