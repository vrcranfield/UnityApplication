using UnityEngine;

public static class TransformDeepChildExtension
{
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = child.FindDeepChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }
}

public static class Vector3ReciprocalExtension
{
    public static Vector3 Reciprocal(this Vector3 input)
    {
        return new Vector3(1f / input.x, 1f / input.y, 1f / input.z);
    }
}