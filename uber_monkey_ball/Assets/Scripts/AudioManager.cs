using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // This declares an array of the "Sound" class which was previously made.
    public Sound[] sounds;
    
    // We want one instance of this to carry over to other scenes so music doesn't get interrupted.
    public static AudioManager instance; 

    void Awake()
    {
        // When entering a new scene, a duplicate AudioManager will not be created.
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Determine size of sounds in inspector. 
        // For each element in sounds, there will be a field for a name, a clip, volume, pitch, and loop in the inspector. 
        // This is based on what is in the 'Sound' class.
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>(); // For each element in the array we need an AudioSource.

            // This sets the parameters of the AudioSource component to be equal to what we preset it to be in the inspector for each element.
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("MusicSbS"); // Play the music once the level starts. If it is already playing it should not restart.
    }

    // This method is called when events occur, and it will play the associated audio clip.
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    public void PlayThud(float collisionForce)
    {
        // parameterize thud pitch and volume with collision Force
        Sound s = Array.Find(sounds, sound => sound.name == "Thud");
        float pitch = 2 - 0.05f * collisionForce;
        s.source.pitch = Mathf.Clamp(pitch, 0.7f, 1.5f);
        s.source.volume = Mathf.Clamp(collisionForce/20, 0.4f, 1f);
        Debug.Log(s.source.pitch + " " + s.source.volume);
        s.source.Play();
    }
}