using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource Source;
    private AudioClip Track;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Source = GetComponent<AudioSource>();
    }

    void Start()
    {
        Source.loop = true;
    }

    void Update()
    { 
    }

    public void Play(AudioClip track)
    {
        try
        {
            if (Track.Equals(track) && Source.isPlaying) return;
        } catch (NullReferenceException ex)
        {
            Debug.LogError(ex);
        } finally
        {
            Track = track;
        }

        Source.clip = track;
        Source.Play();
    }

    public void Stop() => Source.Stop();
}
