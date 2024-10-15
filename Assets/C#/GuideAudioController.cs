using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideAudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] guideAudios;

    // record played audio
    private bool[] hasPlayed; 

    public static GuideAudioController Instance;

    void Start()
    {
        Instance = this;

        
        hasPlayed = new bool[guideAudios.Length];
    }

   
    public void PlayAudio(int i)
    {
        // check if is played
        if (!hasPlayed[i])
        {
            audioSource.clip = guideAudios[i];
            audioSource.Play();

            // mark played audio
            hasPlayed[i] = true;
            Debug.Log("Playing audio: " + i);
        }
        else
        {
            Debug.Log("Audio " + i + " has already been played.");
        }
    }
}