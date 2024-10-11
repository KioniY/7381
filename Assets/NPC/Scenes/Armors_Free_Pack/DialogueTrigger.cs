using UnityEngine;
using UnityEngine.UI;  // 用于UI组件
using TMPro;  // 如果使用TextMeshPro，可以使用这个命名空间

public class PlayerSelfTalk : MonoBehaviour
{
    public Text dialogueText;  // 如果你使用的是普通Text组件
    public TMP_Text tmpDialogueText;  // 如果你使用的是TextMeshPro
    public AudioSource audioSource;  // 用于播放语音
    public AudioClip voiceClip;  // 语音文件
    public float displayDuration = 5f;  // 显示文本的时间长度

    private bool hasMoved = false;  // 用于检测是否已经触发过对话
    private float timeSinceMove = 0f;  // 记录时间

    void Start()
    {
        // 初始化时，隐藏文本
        if (dialogueText != null) dialogueText.gameObject.SetActive(false);
        if (tmpDialogueText != null) tmpDialogueText.gameObject.SetActive(false);
    }

    void Update()
    {
        // 检测玩家是否移动
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (!hasMoved && (moveX != 0 || moveZ != 0)) // 玩家按下WASD移动
        {
            hasMoved = true;  // 设为true，防止重复触发
            TriggerSelfTalk();  // 触发自言自语
        }

        // 如果已经触发对话，开始计时隐藏文本
        if (hasMoved)
        {
            timeSinceMove += Time.deltaTime;
            if (timeSinceMove >= displayDuration)
            {
                if (dialogueText != null) dialogueText.gameObject.SetActive(false);
                if (tmpDialogueText != null) tmpDialogueText.gameObject.SetActive(false);
            }
        }
    }

    void TriggerSelfTalk()
    {
        // 显示对话文本
        string dialogue = "Another peaceful night... Rome sleeps, and only we remain on guard.";

        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(true);
            dialogueText.text = dialogue;
        }

        if (tmpDialogueText != null)
        {
            tmpDialogueText.gameObject.SetActive(true);
            tmpDialogueText.text = dialogue;
        }

        // 播放语音
        if (audioSource != null && voiceClip != null)
        {
            audioSource.PlayOneShot(voiceClip);  // 播放语音
        }
    }
}
