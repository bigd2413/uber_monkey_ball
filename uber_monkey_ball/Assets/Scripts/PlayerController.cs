using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void FalloutEventHandler();
public class PlayerController : MonoBehaviour
{
    private bool isGrounded;
    Rigidbody rb;
    AudioSource audioSource;
    public event FalloutEventHandler PlayerFalloutEvent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 20f;
        audioSource = GetComponent<AudioSource>();
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

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void Update()
    {
        if (isGrounded)
        {
        if (rb.velocity.magnitude >= 1)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.pitch = 0.15f * rb.velocity.magnitude + 0.3f;
                audioSource.pitch = Mathf.Clamp(audioSource.pitch, 0.3f, 2f);
                audioSource.Play();
            }
        }
        else
            audioSource.Stop();
        }




        isGrounded = false;
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
