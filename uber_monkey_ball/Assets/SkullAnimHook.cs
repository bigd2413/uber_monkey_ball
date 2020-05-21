using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullAnimHook : MonoBehaviour
{
    Animator animator;
    Rigidbody rigidbody;
    string animatorParameter = "Blend";
    Quaternion lastFrameRot;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float speed = rigidbody.velocity.magnitude;
        animator.SetFloat(animatorParameter, speed);
    }
}
