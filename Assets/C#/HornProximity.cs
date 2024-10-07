using UnityEngine;

public class HornProximity : MonoBehaviour
{
    public Transform leftHand;        // 左手控制器的 Transform
    public Transform rightHand;       // 右手控制器的 Transform
    public AudioSource hornSound;     // 号角的 AudioSource
    public float triggerDistance = 2f;  // 播放声音的距离阈值
    private bool isPlaying = false;     // 记录音效是否正在播放

    void Start()
    {
        // 如果没有手动设置，自动查找左右手控制器的 Transform
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
        // 计算号角与左手和右手的距离
        float distanceToLeftHand = Vector3.Distance(transform.position, leftHand.position);
        float distanceToRightHand = Vector3.Distance(transform.position, rightHand.position);

        // 实时记录左右手的位置（可选调试信息）
        Debug.Log("Left Hand Position: " + leftHand.position);
        Debug.Log("Right Hand Position: " + rightHand.position);

        // 如果任何一个手柄距离号角小于阈值且声音没有播放，播放声音
        if ((distanceToLeftHand < triggerDistance || distanceToRightHand < triggerDistance) && !isPlaying)
        {
            Debug.Log("Playing horn sound");
            hornSound.Play();
            isPlaying = true;
        }

        // 如果所有手柄距离号角大于阈值且声音正在播放，停止声音
        if (distanceToLeftHand >= triggerDistance && distanceToRightHand >= triggerDistance && isPlaying)
        {
            Debug.Log("Stopping horn sound");
            hornSound.Stop();
            isPlaying = false;
        }
    }
}