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
            StartCoroutine(GetBoneRoutine(other.transform));
        }
    }
    IEnumerator GetBoneRoutine(Transform player)
    {
        audioSource.Play();
        BoneAnimator.SetTrigger("GetBone");
        float t = 0;
        while (t < 2)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, 0.2f);
            t += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
