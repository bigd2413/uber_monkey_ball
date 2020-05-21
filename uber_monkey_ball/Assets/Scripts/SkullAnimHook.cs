using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullAnimHook : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;
    string animatorParameter = "Blend";
    Quaternion lastFrameRot;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float speed = rb.velocity.magnitude;
        animator.SetFloat(animatorParameter, speed);
    }
}
