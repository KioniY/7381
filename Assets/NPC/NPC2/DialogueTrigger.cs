using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Transform leftHand;        // 左手控制器的 Transform
    public Transform rightHand;       // 右手控制器的 Transform
    public GameObject target;         // 对话触发点的目标物体
    public float triggerDistance = 2f;  // 触发对话的距离阈值

    private bool isDialogueTriggered = false; // 标记对话是否已经触发

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
        if (!isDialogueTriggered)
        {
            // 计算左右手控制器与目标点的距离
            float leftHandDistance = Vector3.Distance(leftHand.position, target.transform.position);
            float rightHandDistance = Vector3.Distance(rightHand.position, target.transform.position);

            // 如果任意手控制器接近目标点，触发对话
            if (leftHandDistance <= triggerDistance || rightHandDistance <= triggerDistance)
            {
                TriggerDialogue();  // 触发对话
                isDialogueTriggered = true; // 标记为已触发，防止重复触发
            }
        }
    }

    void TriggerDialogue()
    {
        // 守卫A对话
        Debug.Log("A fire this big must have been started by someone.");

        // 等待 2 秒后触发守卫B对话
        Invoke("GuardBDialogue", 2f);
    }

    void GuardBDialogue()
    {
        Debug.Log("It’s definitely those damned Christians who did it.");
    }
}


