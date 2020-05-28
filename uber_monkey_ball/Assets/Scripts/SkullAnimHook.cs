using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullAnimHook : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    string animatorParameter = "Blend";
    Vector3 smoothedVelocity;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Correct to stay upright and keep animations
        Quaternion animRotOffset = transform.localRotation;
        smoothedVelocity = 0.1f * rb.velocity + 0.9f * smoothedVelocity;
        if (smoothedVelocity.magnitude > 0.0001f)
        {
            float maxVelWithControl = 16;
            // From 0 to maxVel => full control, maxVel to 4*maxVel => linear descent to 0. Output Clamp 0 to 1
            float correctionMagnitude = Mathf.Clamp01(-0.5f/maxVelWithControl *Mathf.Max(0,smoothedVelocity.magnitude-maxVelWithControl) + 1);
            // Slerp between no rotation and full corrective rotation
            Quaternion correctionRot = Quaternion.Slerp(Quaternion.identity,Quaternion.FromToRotation(transform.forward,smoothedVelocity),correctionMagnitude);
            transform.forward = correctionRot*transform.forward;
            transform.forward = animRotOffset * transform.forward;
        }

        // Blend anims with speed
        float speed = rb.velocity.magnitude;
        animator.SetFloat(animatorParameter, speed);
    }
}
