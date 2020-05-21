using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidbodyHook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = 20f;
    }
}
