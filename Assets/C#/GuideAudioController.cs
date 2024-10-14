using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideAudioController : MonoBehaviour
{
    public  AudioSource audioSource;

    public AudioClip[] guideAudios;
    // Start is called before the first frame update

    public static GuideAudioController Instance;
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio(int i)
    {
        audioSource.clip = guideAudios[i];
        audioSource.Play();
    }
}
