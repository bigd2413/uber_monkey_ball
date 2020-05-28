using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PRECONDITIONS:
//      For levels with nested children objects(almost all levels). A rigidbody with IsKinematic true is required. This ensures the level is "rigid". If this
//      if not done. Players sprials out of control. It seems the Rigidbody component truely forces the object and its children to be a rigidbody
public class LevelController : MonoBehaviour
{
    //Interface
    public Transform player;
    public float maxTilt;
    public Transform mainCamera;
    public bool controlActive;
    
    //Protected
    Transform levelTransform;

    public Transform gameManager;

    // Start is called before the first frame update
    private void Awake()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>() as Rigidbody;
        rb.isKinematic = true;
        rb.useGravity = false;
    }
    void Start()
    {
        levelTransform = transform;

        Timer timerScript = gameManager.GetComponent<Timer>();
        timerScript.TimeoutEvent += LevelStop;
        controlActive = true;
    }

    Quaternion cachedPlayerRot;
    Vector3 smoothInput;
    Vector3 input;
    void FixedUpdate()
    {
        cachedPlayerRot = player.rotation;
        player.rotation = Quaternion.identity;

        if (controlActive == true)
        {
            input = new Vector3(Input.GetAxis("Vertical"),0, - Input.GetAxis("Horizontal"));
        }
        else
        {
            input = new Vector3 (0,0,0);
        }
        smoothInput = input / 4 + smoothInput * 3 / 4;
        Vector3 inputCamSpace = mainCamera.TransformDirection(smoothInput);
        Quaternion cameraPitchRot = Quaternion.FromToRotation(mainCamera.up, Vector3.up);
        inputCamSpace = cameraPitchRot * inputCamSpace;
        Quaternion offsetRot = Quaternion.AngleAxis(smoothInput.magnitude * maxTilt, inputCamSpace);        
        levelTransform.SetParent(player);
        player.rotation = Quaternion.Inverse(levelTransform.rotation) * player.rotation;
        player.rotation = offsetRot * player.rotation;
        levelTransform.parent = null;
        player.rotation = cachedPlayerRot;
    }

    public void LevelStop()
    {
        controlActive = false;
        return;
    }
}
