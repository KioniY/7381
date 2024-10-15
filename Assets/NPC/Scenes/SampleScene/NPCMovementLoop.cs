using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovementLoop : MonoBehaviour
{
    public Transform startPoint; // 起始位置
    public Transform targetPoint; // 目标位置
    public float waitTimeAtTarget = 0.0f; // 到达目标后等待的时间
    public float startDelay = 12.0f; // 延迟时间，12秒后开始

    private NavMeshAgent agent;
    private Animator animator;
    private MeshRenderer[] meshRenderers; // 存储MeshRenderers以控制显示
    private bool isWaiting = false; // 是否在等待
    private float waitTimer = 0f; // 计时器

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        // 禁用移动和动画，等待12秒后再启用
        agent.enabled = false;
        animator.SetFloat("Speed", 0f); // 禁用动画一开始的播放

        // 开始计时，12秒后移动NPC并启动动画
        StartCoroutine(DelayedStartMovement());
    }

    void Update()
    {
        if (agent.enabled)
        {
            // 更新动画的Speed参数
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed); // 设置Speed以控制跑步动画

            // 检查是否到达目标点
            if (!agent.pathPending && agent.remainingDistance < 0.5f && !isWaiting)
            {
                // 到达目标点，开始等待并消失
                isWaiting = true;
                waitTimer = waitTimeAtTarget;
                HideNPC(); // 隐藏NPC，但保持其活动状态
            }

            // 如果正在等待，则倒计时
            if (isWaiting)
            {
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    // 等待结束，重新出现并从起点出发
                    ReappearAtStart();
                    isWaiting = false;
                }
            }
        }
    }

    // 延迟12秒后开始移动NPC并启用动画
    IEnumerator DelayedStartMovement()
    {
        yield return new WaitForSeconds(startDelay); // 延迟12秒
        agent.enabled = true; // 启用NavMeshAgent
        animator.SetFloat("Speed", 1f); // 开始动画的播放
        StartMovement();
    }

    void StartMovement()
    {
        // 设置新的目标为targetPoint
        agent.SetDestination(targetPoint.position);
        ShowNPC(); // 显示NPC
    }

    void HideNPC()
    {
        // 隐藏NPC的可见性，但保持对象活跃状态
        foreach (var renderer in meshRenderers)
        {
            renderer.enabled = false;
        }
    }

    void ReappearAtStart()
    {
        // 让NPC重新出现在起始位置
        foreach (var renderer in meshRenderers)
        {
            renderer.enabled = true;
        }
        transform.position = startPoint.position; // 重置到起始位置
        StartMovement(); // 再次移动到目标点
    }

    void ShowNPC()
    {
        // 显示NPC
        foreach (var renderer in meshRenderers)
        {
            renderer.enabled = true;
        }
    }
}
