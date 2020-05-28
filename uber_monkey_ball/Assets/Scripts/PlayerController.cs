using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void FalloutEventHandler();
public class PlayerController : MonoBehaviour
{
    private bool isGrounded;
    private bool thud;
    private float thudCoolDown;
    public Transform gameManager;

    Rigidbody rb;
    AudioSource audioSource;
    public event FalloutEventHandler PlayerFalloutEvent;


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
            GameManager.Instance.ManageGoal();
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
        float maxDotProduct = -1f; 

        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;

            float dotProduct = Mathf.Abs(Vector3.Dot(rb.velocity.normalized, normal)*Mathf.Rad2Deg);

            if (dotProduct > maxDotProduct)
            {
                maxDotProduct = dotProduct;
            }
        }

        if (maxDotProduct > 30f && rb.velocity.magnitude > 5f && thudCoolDown <= 0f)
            thud = true;
        else
            thud = false;
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

        if (thud)
        {
            thudCoolDown = 0.8f;
            FindObjectOfType<AudioManager>().Play("Thud");
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
