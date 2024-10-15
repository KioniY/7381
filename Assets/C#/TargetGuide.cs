using UnityEngine;
using UnityEngine.Video;

public class TargetGuide : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;
    public GameObject[] targets;
    public float triggerDistance = 2f;
    public GameObject plane;
    public VideoPlayer videoPlayer;

    public AudioClip combinedAudioClip;  
    private AudioSource audioSource;  
    private bool isAudioPlayed = false;  

    private int currentTargetIndex = 0;

    void Start()
    {
        // show the first point
        for (int i = 1; i < targets.Length; i++)
        {
            targets[i].SetActive(false);
        }

        // controller
        if (leftHand == null)
        {
            leftHand = GameObject.Find("LeftHandAnchor").transform;
        }

        if (rightHand == null)
        {
            rightHand = GameObject.Find("RightHandAnchor").transform;
        }

        
        if (plane != null)
        {
            plane.SetActive(false);
        }

       
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
        }

        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on this GameObject!");
        }
    }

    void Update()
    {
       
        if (currentTargetIndex >= targets.Length)
        {
            return;
        }

        // calculate the distance
        float distanceToLeftHand = Vector3.Distance(leftHand.position, targets[currentTargetIndex].transform.position);
        float distanceToRightHand = Vector3.Distance(rightHand.position, targets[currentTargetIndex].transform.position);

        
        if (distanceToLeftHand < triggerDistance || distanceToRightHand < triggerDistance)
        {
            targets[currentTargetIndex].SetActive(false);  
            currentTargetIndex++; 

            // to the forth point, play video
            if (currentTargetIndex == 4)
            {
                PlayVideoOnPlane();
            }

            // to the sixth point, play audio
            if (currentTargetIndex == 6 && !isAudioPlayed)
            {
                PlayCombinedAudio();
            }

            // next point
            if (currentTargetIndex < targets.Length)
            {
                targets[currentTargetIndex].SetActive(true);
            }

            Debug.Log("Hand reached target " + currentTargetIndex);
        }
    }

    // play video
    void PlayVideoOnPlane()
    {
        if (plane != null && videoPlayer != null)
        {
            plane.SetActive(true);
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnVideoEnd;  // 视频结束时回调
            Debug.Log("Playing video on plane at target 4.");
        }
    }

    // hide plane
    void OnVideoEnd(VideoPlayer vp)
    {
        if (plane != null)
        {
            plane.SetActive(false);
            Debug.Log("Video finished, plane hidden.");
        }
    }

    // play audio
    void PlayCombinedAudio()
    {
        if (combinedAudioClip != null)
        {
            audioSource.clip = combinedAudioClip;  
            audioSource.time = 0f;  
            audioSource.Play();
            isAudioPlayed = true;  
            Debug.Log("Playing combined audio at target 6.");
        }
    }
}
