using UnityEngine;

/**
 * Utility static class that provides additional features to 
 * Unity's Transform class
 */
public static class TransformDeepChildExtension
{
    // Search descendant by name
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

/**
 * Utility static class that provides additional features to 
 * Unity's Vector3 class
 */
public static class Vector3ReciprocalExtension
{
    /* 
     * Return reciprocal vector
     */
    public static Vector3 Reciprocal(this Vector3 input)
    {
        return new Vector3(1f / input.x, 1f / input.y, 1f / input.z);
    }
}