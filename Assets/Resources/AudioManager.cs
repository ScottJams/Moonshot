using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    // Singleton
    private static AudioManager _instance;
    public static AudioManager SharedInstance { get { return _instance; } }

    // Sound effects library
    public AudioClip jumpEffect;
    public AudioClip dashEffect;
    public AudioClip dieEffect;
    public AudioClip collectMoonEffect;

    // Enforce singleton on awake
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(SoundEffect soundEffect)
    {
        switch (soundEffect) {
            case SoundEffect.jump:
                audioSource.PlayOneShot(jumpEffect); 
                break;
            case SoundEffect.dash:
                audioSource.PlayOneShot(dashEffect);
                break;
            case SoundEffect.collectMoon:
                audioSource.PlayOneShot(collectMoonEffect);
                break;
            case SoundEffect.death:
                audioSource.PlayOneShot(dieEffect);
                break;
        }

    }

}

public enum SoundEffect
{
    jump,
    dash,
    collectMoon,
    death
}
