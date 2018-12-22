using UnityEngine;
using System.Collections;

public static class TransformExtensions {

    public static void SmoothRotate(this Transform transform, Vector3 target, float turnSpeed, bool x = true, bool y = true, bool z = true) {
        float xTarget = x ? transform.position.x : target.x;
        float yTarget = x ? transform.position.y : target.y;
        float zTarget = x ? transform.position.z : target.z;

        Vector3 targetPoint = new Vector3(xTarget, yTarget, zTarget) - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
