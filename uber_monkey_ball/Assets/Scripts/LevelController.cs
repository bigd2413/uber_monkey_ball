using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    //Interface
    public Transform player;
    public float maxTilt;
    public Transform mainCamera;
    
    //Protected
    Transform levelTransform;
    // Start is called before the first frame update
    void Start()
    {
        levelTransform = transform;
    }

    // Update is called once per frame
    Quaternion cachedPlayerRot;
    Vector3 smoothInput;
    void FixedUpdate()
    {
        cachedPlayerRot = player.rotation;
        player.rotation = Quaternion.identity;
        Vector3 input = new Vector3(Input.GetAxis("Vertical"),0, -Input.GetAxis("Horizontal"));
        smoothInput = input / 4 + smoothInput * 3 / 4;
        Vector3 inputCamSpace = mainCamera.TransformDirection(smoothInput);
        Quaternion cameraPitchRot = Quaternion.FromToRotation(mainCamera.up, Vector3.up);
        inputCamSpace = cameraPitchRot * inputCamSpace;
        Quaternion offsetRot = Quaternion.AngleAxis(smoothInput.magnitude * maxTilt, inputCamSpace);
        levelTransform.SetParent(player);
        player.rotation = Quaternion.Inverse(levelTransform.rotation) * player.rotation;
        player.rotation = offsetRot * player.rotation;
        player.DetachChildren();
        player.rotation = cachedPlayerRot;
    }
    private void LateUpdate()
    {
       
    }
}
