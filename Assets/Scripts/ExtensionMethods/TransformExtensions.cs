using UnityEngine;

public static class TransformExtensions {

    public static void SmoothRotate(this Transform transform, Vector3 target, float turnSpeed, bool x = true, bool y = true, bool z = true) {
        float xTarget = x ? target.x : transform.position.x;
        float yTarget = y ? target.y : transform.position.y;
        float zTarget = z ? target.z : transform.position.z;

        Vector3 targetPoint = new Vector3(xTarget, yTarget, zTarget) - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(targetPoint, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public static void ResetTransform(this Transform transform) {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        //transform.scale = Vector3.one;
    }
}
