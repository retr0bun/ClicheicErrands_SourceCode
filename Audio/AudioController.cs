using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioController : MonoBehaviour
{

    public SOUNDS[] sounds;

    public static AudioController instance;
    
    void Awake()
    {
        //if there is no other instance make this the instance
        //and if there is destroy this one
        if (instance == null)
        {
            instance = this;
        }else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (SOUNDS s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    void Start()
    {
        Play("main theme");
    }
    
    public void Play (string name)
    {
        SOUNDS s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }    
        s.source.Play();
    }
    public void Pause(string name)
    {
        SOUNDS s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Pause();
    }
}