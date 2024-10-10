using UnityEngine;

public class TargetGuide : MonoBehaviour
{
    public Transform leftHand;  // left hand Transform
    public Transform rightHand; // right hand Transform
    public GameObject[] targets;  
    public float triggerDistance = 2f;  // 

    private int currentTargetIndex = 0;  // 

    void Start()
    {
        // 
        for (int i = 1; i < targets.Length; i++)
        {
            targets[i].SetActive(false);  
        }

        
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
    {   //return if all targets achieved
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

            if (currentTargetIndex < targets.Length)
            {
                targets[currentTargetIndex].SetActive(true);
            }

            Debug.Log("Hand reached target " + currentTargetIndex);
        }
    }
}