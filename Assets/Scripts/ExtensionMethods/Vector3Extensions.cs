using UnityEngine;
using System.Collections;

public static class Vector3Extensions {

    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null) {
        float newX = x.HasValue ? (float)x : original.x;
        float newY = y.HasValue ? (float)y : original.y;
        float newZ = z.HasValue ? (float)z : original.z;

        return new Vector3(newX, newY, newZ);
    }
}
