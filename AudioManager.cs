using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip Blowup;
    public AudioClip Catch;
    public AudioClip Chunks;
    public AudioClip Teleport;
    public AudioClip Throw;
    public AudioClip Win;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayBlowup()
    {
        audioSource.PlayOneShot(Blowup);
    }
    public void PlayCatch()
    {
        audioSource.PlayOneShot(Catch);
    }
    public void PlayChunks()
    {
        audioSource.PlayOneShot(Chunks);
    }
    public void PlayTeleport()
    {
        audioSource.PlayOneShot(Teleport);
    }
    public void PlayThrow()
    {
        audioSource.PlayOneShot(Throw);
    }
    public void PlayWin()
    {
        audioSource.PlayOneShot(Win);
    }
}
