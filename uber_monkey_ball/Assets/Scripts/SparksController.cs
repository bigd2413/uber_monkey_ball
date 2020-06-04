using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksController : MonoBehaviour
{
    Rigidbody rb;
    float speed;
    Vector3 heading;
    public ParticleSystem sparkPS1;
    public ParticleSystem sparkPS2;
    ParticleSystem[] sparkParticleSystems = new ParticleSystem[2];
    //Controls the amount of the particles based on the rb speed
    public AnimationCurve particleSpeedCurve;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        sparkParticleSystems[0] = sparkPS1;
        sparkParticleSystems[1] = sparkPS2;
    }

    // Update is called once per frame
    void Update()
    {
        speed = rb.velocity.magnitude;
        heading = rb.velocity.normalized;

        // Orient Particles System Transform
        transform.up = Vector3.up;
        transform.forward = -heading; //Side effect of Up now aligning with velocity
        transform.position = transform.parent.position - transform.up*.5f; //Bring to floor.

        // Toggle Particles on and off

        if (sparkPS1.isEmitting && speed < 10)
        {
            foreach (var ps in sparkParticleSystems)
            {
                ps.Stop(true,ParticleSystemStopBehavior.StopEmitting);
            }
        }
        else if (!sparkPS1.isEmitting && speed >6)
        {
            foreach (var ps in sparkParticleSystems)
            {
                ps.Play();
            }
        }

        // Adjust Speed of Particles
        foreach (var ps in sparkParticleSystems)
        {
            ps.startSpeed = speed/2;
        }
    }
}
