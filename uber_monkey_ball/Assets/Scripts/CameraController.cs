using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform focus;
    [Tooltip("Orbit distance from target")]
    [SerializeField,Range(1f,50f)]
    public float orbitDistance = 1f;
    [Tooltip("The radius from focus that which when the camera is outside of, will begin to move the camera to focus")]
    [SerializeField, Range(0.1f, 4f)]
    public float focusRadius = 1f;
    [Tooltip("The strength of the focus centering effect. Centers the focus gradually on the target")]
    [SerializeField, Range(0f, 1f)]
    float focusCentering = 0.5f;
    private Vector3 velocity;
    [Tooltip("Speed of Camera Rotations, manual and automatic")]
    [SerializeField, Range(5f, 100f)]
    public float cameraRotateSpeed = 40f;
    float camRotate;


    // Start is called before the first frame update
    Vector3 focusPoint;
    public Vector2 orbitAngles = new Vector2(30f,0);
    void Start()
    {
        focusPoint = focus.position;
    }

    void ManualRotation()
    {
        Vector2 input = new Vector2(Input.GetAxis("CameraPitch"), Input.GetAxis("CameraYaw"));
        float e = 0.15f;
        if (input.magnitude > e)
        {
            orbitAngles += input * cameraRotateSpeed * Time.unscaledDeltaTime;
        }
    }
    void UpdateFocusPoint()
    {
        Vector3 targetPoint = focus.position;
        float distance = Vector3.Distance(targetPoint, focusPoint);
        if (distance > focusRadius)
        {
            focusPoint = Vector3.Lerp(targetPoint, focusPoint, focusRadius / distance);
        }
        if (distance > 0.05f && focusRadius>0.05f)
        {
            focusPoint = Vector3.Lerp(targetPoint, focusPoint, Mathf.Pow(1-focusCentering, Time.unscaledDeltaTime));
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateFocusPoint();
        ManualRotation();
        Quaternion lookRotation = Quaternion.Euler(orbitAngles);
        Vector3 lookDirection = lookRotation*Vector3.forward;
        Vector3 lookPosition = focusPoint - orbitDistance * lookDirection;
        transform.SetPositionAndRotation(lookPosition,lookRotation);
    }
}
