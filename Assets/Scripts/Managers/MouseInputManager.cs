using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MouseInputManager : Singleton<MouseInputManager>
{
    private Camera mainCamera;

    [SerializeField]
    private LayerMask layerMask = 1;
    const float MAX_DISTANCE = 100f;

    public Vector3 MouseWorldPosition { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        GetMouseWorldPosition();
    }

    private void GetMouseWorldPosition() {
        Ray ray = mainCamera.ScreenPointToRay(CrossPlatformInputManager.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, MAX_DISTANCE, layerMask)) {
            MouseWorldPosition = hitInfo.point;
            //Debug.DrawRay(ray.origin, ray.direction * MAX_DISTANCE);
            //Debug.DrawRay(MouseWorldPosition, Vector3.up * 20, Color.blue);
        }
    }
}
