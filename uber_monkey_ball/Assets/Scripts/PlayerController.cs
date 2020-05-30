using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void FalloutEventHandler();
public delegate void GoalEventHandler();

public class PlayerController : MonoBehaviour
{
    private bool isGrounded;
    private bool thud;
    private float thudForce;
    private float thudCoolDown;
    public Transform gameManager;

    Rigidbody rb;
    AudioSource audioSource;
    public event FalloutEventHandler PlayerFalloutEvent;
    public event GoalEventHandler GoalEvent;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 20f;
        audioSource = GetComponent<AudioSource>();
        thudCoolDown = 0;

        Timer timerScript = gameManager.GetComponent<Timer>();
        timerScript.TimeoutEvent += TimeoutProcedure;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            rb.useGravity = false;
            rb.drag = 1f;
            rb.angularDrag = 1f;
            StartCoroutine(FlyAway());
            GoalEvent?.Invoke();
        }
        if (other.CompareTag("Falloff"))
        {
            GameManager.Instance.ManageFalloff();
            PlayerFalloutEvent?.Invoke();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
        EvaluateCollision(collision);
    }

    void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }

    void EvaluateCollision(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;

            float dotProduct = Vector3.Dot(rb.velocity.normalized, normal.normalized);

            if (dotProduct < -0.2f && rb.velocity.magnitude * -1 * dotProduct > 5f && thudCoolDown <=0f)
            {
                thudForce = rb.velocity.magnitude * -1 * dotProduct;
                thud = true;
            }
        }
    }

    private void Update()
    {
        if (isGrounded)
        {
        if (rb.velocity.magnitude >= 1)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.pitch = 0.125f * rb.velocity.magnitude + 0.2f;
                audioSource.pitch = Mathf.Clamp(audioSource.pitch, 0.3f, 2f);
                audioSource.Play();
            }
        }
        else
            audioSource.Stop();
        }

        if (thud)
        {
            thudCoolDown = 0.5f;
            FindObjectOfType<AudioManager>().PlayThud(thudForce);
            thud = false;
        }

        if (thudCoolDown > 0f)
            thudCoolDown -= Time.deltaTime;

        isGrounded = false;
    }

    IEnumerator FlyAway()
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

    public void TimeoutProcedure()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");

        rb.useGravity = false;
        rb.drag = 1f;
        rb.angularDrag = 1f;

        StartCoroutine(FlyAway());

        return;
    }


}
