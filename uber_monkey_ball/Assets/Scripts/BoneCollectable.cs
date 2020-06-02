using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneCollectable : MonoBehaviour
{
    public AudioSource audioSource;
    Animator BoneAnimator;
    public GameStats gameStats;
    bool isPickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        BoneAnimator = GetComponent<Animator>();

        StartCoroutine(FindGameStats());
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            // Play GetBone animation
            isPickedUp = true;
            StartCoroutine(GetBoneRoutine(other.transform));
        }
    }
    IEnumerator GetBoneRoutine(Transform player)
    {
        audioSource.Play();
        BoneAnimator.SetTrigger("GetBone");

        if (gameObject.tag == "MultiBone")
        {
            gameStats.IncreaseBoneCount(3);
        }
        else
        {
            gameStats.IncreaseBoneCount(1);
        }

        float t = 0;
        while (t < 2)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, 0.2f);
            t += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    IEnumerator FindGameStats()
    {
        yield return new WaitForEndOfFrame();
        gameStats = FindObjectOfType<GameStats>();
    }
}
