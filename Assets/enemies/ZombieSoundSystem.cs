using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSoundSystem : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioClip[] hit_clips;
    public AudioClip dying_clip;
    private AudioSource[] audioSource;


    void Start()
    {
        audioSource = GetComponents<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing from this GameObject");
            return;
        }

        PlayRandomClip();
    }

    public void PlayRandomClip()
    {
        if (clips.Length == 0) return;

        int randomIndex = Random.Range(0, clips.Length);
        audioSource[0].clip = clips[randomIndex];
        audioSource[0].Play();

    }

    public void PlayDyingClip()
    {
        audioSource[0].Stop();
        audioSource[0].clip = dying_clip;
        audioSource[0].volume = 1.0f;
        audioSource[0].Play();
    }

    public void PlayHitClip()
    {
        if (hit_clips.Length == 0) return;

        int randomIndex = Random.Range(0, hit_clips.Length);
        audioSource[1].clip = hit_clips[randomIndex];
        audioSource[1].Play();
    }
}
