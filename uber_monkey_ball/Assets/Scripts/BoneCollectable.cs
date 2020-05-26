using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCollectable : MonoBehaviour
{
    public AudioSource audioSource;
    Animator BoneAnimator;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        BoneAnimator = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play GetBone animation
            StartCoroutine(GetBoneRoutine());
        }
    }
    IEnumerator GetBoneRoutine()
    {
        audioSource.Play();
        BoneAnimator.SetTrigger("GetBone");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
