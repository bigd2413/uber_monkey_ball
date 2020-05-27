using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] // This makes it so our parameters will show up in the inspector.
public class Sound 
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    [HideInInspector] // Without this, we could have a bunch of clutter in our inspector.
    public AudioSource source;

    public bool loop;
}
