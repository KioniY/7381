using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayerControl1 : MonoBehaviour
{
    private Animator animator;
    private bool isTorchOut = false;  // 记录火把是否在手中
    public GameObject torch;  // 火把对象
    public Transform playerHand;  // 玩家手部的位置

    // 玩家移动相关参数
    public float walkSpeed = 2f;  // 走动速度
    private CharacterController characterController;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        // 确保火把初始为隐藏状态
        torch.SetActive(false);
    }

    void Update()
    {
        // 获取玩家输入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(horizontal, 0, vertical);

        // 控制玩家的移动和动画
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            characterController.Move(dir * walkSpeed * Time.deltaTime);  // 控制移动
            animator.SetBool("move", true);
        }
        else
        {
            animator.SetBool("move", false);
        }

        // 判断是否按下 Y 键来掏出或收回火把
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ToggleTorch();
        }
    }

    // 切换火把的状态（掏出/收回）
    void ToggleTorch()
    {
        if (isTorchOut)
        {
            // 收回火把
            animator.SetTrigger("hideTorch");  // 播放收回火把的动画
            torch.SetActive(false);  // 隐藏火把
        }
        else
        {
            // 掏出火把
            animator.SetTrigger("drawTorch");  // 播放掏出火把的动画
            torch.SetActive(true);  // 显示火把
            torch.transform.SetParent(playerHand);  // 将火把附加到玩家的手部
            torch.transform.localPosition = Vector3.zero;  // 调整火把的位置
            torch.transform.localRotation = Quaternion.identity;  // 重置火把的旋转
        }

        // 切换火把状态
        isTorchOut = !isTorchOut;
    }
}