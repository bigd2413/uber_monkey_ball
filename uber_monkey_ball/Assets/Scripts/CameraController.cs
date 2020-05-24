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

    [Tooltip("Offset of focus point relative to player")]
    public Vector3 offset = Vector3.zero;

    [Tooltip("Speed of Camera Rotations, manual and automatic")]
    [SerializeField, Range(5f, 1000f)]
    public float cameraRotateSpeed = 250f;

    [Tooltip("Yaw rotation angle at which camera will rotate at full speed.")]
    [SerializeField, Range(0f, 90f)]
    float alignSmoothRange = 45f;


    Vector3 focusPoint, previousFocusPoint;

    public Vector2 orbitAngles = new Vector2(30f,0);

    // Start is called before the first frame update
    void Start()
    {
        focusPoint = focus.position;
    }

    // This updates where the camera is looking at
    void UpdateFocusPoint()
    {
        previousFocusPoint = focusPoint;
        Vector3 targetPoint = focus.position + offset;
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

    // This updates the yaw rotation of the camera based on the movement of the player.
    bool AutomaticRotation()
    {
        Vector2 movement = new Vector2(focusPoint.x - previousFocusPoint.x, focusPoint.z - previousFocusPoint.z); // Camera rotation only cares about 2d movement

        // If the movement is too small no rotation happens.
        float movementDeltaSqr = movement.sqrMagnitude;
        if (movementDeltaSqr < 0.000001f)
        {
            return false;
        }


        float headingAngle = GetAngle(movement / Mathf.Sqrt(movementDeltaSqr)); // Calls GetAngle function to obtain heading 
        float deltaAbs = Mathf.Abs(Mathf.DeltaAngle(orbitAngles.y, headingAngle)); // Difference between current angle and desired        
        float rotationChange = cameraRotateSpeed * Mathf.Min(Time.unscaledDeltaTime, movementDeltaSqr);

        // If the difference between heading (desired) angle and current angle is less than alignSmoothRange, our rotation speed will be slower.
        if(deltaAbs < alignSmoothRange)
        {
            rotationChange *= deltaAbs / alignSmoothRange;
        } else if (180f - deltaAbs < alignSmoothRange)
        {
            rotationChange *= (180f - deltaAbs) / alignSmoothRange;
        }
        orbitAngles.y = Mathf.MoveTowardsAngle(orbitAngles.y, headingAngle, rotationChange);
        return true;
    }

    // This obtains the heading angle
    static float GetAngle (Vector2 direction)
    {
        float angle = Mathf.Acos(direction.y) * Mathf.Rad2Deg;
        return direction.x < 0f ? 360f - angle : angle;
    }

    // This makes sure that the yaw angle is always between 0 and 360.
    void ConstrainAngles()
    {
        if (orbitAngles.y < 0f)
        {
            orbitAngles.y += 360f;
        }
        else if (orbitAngles.y >= 360f)
        {
            orbitAngles.y -= 360f;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdateFocusPoint();
        if (AutomaticRotation())
        {
            ConstrainAngles();
        }
        Quaternion lookRotation = Quaternion.Euler(orbitAngles);
        Vector3 lookDirection = lookRotation*Vector3.forward;
        Vector3 lookPosition = focusPoint - orbitDistance * lookDirection;
        transform.SetPositionAndRotation(lookPosition,lookRotation);
    }
}
