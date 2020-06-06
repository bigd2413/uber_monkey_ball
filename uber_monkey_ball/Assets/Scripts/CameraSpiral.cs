using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpiral : MonoBehaviour
{
    public float duration = 2;
    Spiral spiral;
    public Transform lookTarget;
    float time;
    public float offsetY_Rot;
    public float radius_max;
    public float max_degrees_turned;
    public float height;

    // Start is called before the first frame update
    public void Start()
    {
        spiral = new Spiral(radius_max, max_degrees_turned, height, transform.position, offsetY_Rot) as Spiral;
        time = 0; 
        FindObjectOfType<Timer>().GetComponent<Timer>().TimeStop();
        FindObjectOfType<LevelController>().GetComponent<LevelController>().LevelStop();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float t = 1- Mathf.Clamp01(time / duration);
        transform.position = spiral.SolveFib3D(t);
        transform.LookAt(lookTarget);
        if (t == 0)
        {
            GetComponent<CameraController>().enabled = true;
            this.enabled = false;
            FindObjectOfType<Timer>().GetComponent<Timer>().TimeStart();
            FindObjectOfType<LevelController>().GetComponent<LevelController>().LevelStart();
        }
    }
}
