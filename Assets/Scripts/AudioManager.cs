using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    [Header("Audio")]
    public Sound[] sounds;
    public AudioSource audioSource;
    
    private Dictionary<string, AudioClip> soundDict = new Dictionary<string, AudioClip>();
    
    void Start()
    {
        foreach (var sound in sounds)
            soundDict[sound.name] = sound.clip;
    }
    
    void OnEnable()
    {
        EventManager.OnPlaySound += PlaySound;
    }
    
    void OnDisable()
    {
        EventManager.OnPlaySound -= PlaySound;
    }
    
    void PlaySound(string soundName)
    {
        if (soundDict.ContainsKey(soundName) && audioSource != null)
            audioSource.PlayOneShot(soundDict[soundName]);
    }
}