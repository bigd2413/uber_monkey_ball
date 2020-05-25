using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void FalloutEventHandler();
public class PlayerController : MonoBehaviour
{
    Rigidbody rb;   
    public event FalloutEventHandler PlayerFalloutEvent;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 20f;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            rb.useGravity = false;
            rb.drag = 1f;
            rb.angularDrag = 1f;
            StartCoroutine(PlayerGoalBehaviour());
            GameManager.Instance.ManageGoal();
        }
        if (other.CompareTag("Falloff"))
        {
            GameManager.Instance.ManageFalloff();
            PlayerFalloutEvent?.Invoke();
        }
    }
    IEnumerator PlayerGoalBehaviour()
    {
        float time = 0;
        yield return new WaitForSeconds(2);
        while (time < 10)
        {
            time += Time.deltaTime;
            rb.AddForce(Vector3.up*50);
            yield return null;
        }

    } 
}
