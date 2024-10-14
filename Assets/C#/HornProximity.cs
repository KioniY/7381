using UnityEngine;

public class HornProximity : MonoBehaviour
{
    public Transform leftHand;        
    public Transform rightHand;       
    public AudioSource hornSound;     
    public float triggerDistance = 2f;  
    private bool isPlaying = false;    

    void Start()
    {
       
        if (leftHand == null)
        {
            leftHand = GameObject.Find("LeftHandAnchor").transform;
        }

        if (rightHand == null)
        {
            rightHand = GameObject.Find("RightHandAnchor").transform;
        }
    }

    void Update()
    {
        
        float distanceToLeftHand = Vector3.Distance(transform.position, leftHand.position);
        float distanceToRightHand = Vector3.Distance(transform.position, rightHand.position);

        
        Debug.Log("Left Hand Position: " + leftHand.position);
        Debug.Log("Right Hand Position: " + rightHand.position);

       
        if ((distanceToLeftHand < triggerDistance || distanceToRightHand < triggerDistance) && !isPlaying)
        {
            Debug.Log("Playing horn sound");
            hornSound.Play();
            isPlaying = true;
        }

        // stop audio
        if (distanceToLeftHand >= triggerDistance && distanceToRightHand >= triggerDistance && isPlaying)
        {
            Debug.Log("Stopping horn sound");
            hornSound.Stop();
            isPlaying = false;
        }
    }
}
