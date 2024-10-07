using UnityEngine;

public class HandControllerMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float rotationSpeed = 90.0f;
    public Transform cameraRigTransform;
    public Transform headTransform;

    void Update()
    {
        Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        Vector3 forward = headTransform.forward;
        forward.y = 0;
        Vector3 right = headTransform.right;

        Vector3 moveDirection = (forward * primaryAxis.y + right * primaryAxis.x);
        cameraRigTransform.position += moveDirection * moveSpeed * Time.deltaTime;

        cameraRigTransform.Rotate(0, secondaryAxis.x * rotationSpeed * Time.deltaTime, 0);
    }
}