using UnityEngine;
using UnityEngine.Video;  // 导入Video命名空间

public class TargetGuide : MonoBehaviour
{
    public Transform leftHand;  
    public Transform rightHand; 
    public GameObject[] targets; 
    public float triggerDistance = 2f;  
    public GameObject plane; 
    public VideoPlayer videoPlayer;  

    private int currentTargetIndex = 0;  

    void Start()
    {
        // only show the first spot
        for (int i = 1; i < targets.Length; i++)
        {
            targets[i].SetActive(false);  
        }

        // find handle
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
    }

    void Update()
    {   
       
        if (currentTargetIndex >= targets.Length)
        {
            return;  
        }

       
        float distanceToLeftHand = Vector3.Distance(leftHand.position, targets[currentTargetIndex].transform.position);
        float distanceToRightHand = Vector3.Distance(rightHand.position, targets[currentTargetIndex].transform.position);

     
        if (distanceToLeftHand < triggerDistance || distanceToRightHand < triggerDistance)
        {
           
            targets[currentTargetIndex].SetActive(false);
            currentTargetIndex++;  

          
            if (currentTargetIndex == 4)
            {
                PlayVideoOnPlane();
            }

            
            if (currentTargetIndex < targets.Length)
            {
                targets[currentTargetIndex].SetActive(true);
            }

            Debug.Log("Hand reached target " + currentTargetIndex);
        }
    }

    // play
    void PlayVideoOnPlane()
    {
        if (plane != null && videoPlayer != null)
        {
            plane.SetActive(true);  
            videoPlayer.Play(); 
            videoPlayer.loopPointReached += OnVideoEnd; 
            Debug.Log("Playing video on plane at target 4.");
        }
    }

    // hide plane after finish
    void OnVideoEnd(VideoPlayer vp)
    {
        if (plane != null)
        {
            plane.SetActive(false);  
            Debug.Log("Video finished, plane hidden.");
        }
    }
}
