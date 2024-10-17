using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovementLoop : MonoBehaviour
{
    public Transform startPoint; // initial position
    public Transform targetPoint; // target location
    public float waitTimeAtTarget = 0.0f; // The waiting time after reaching the target
    public float startDelay = 12.0f; // Delay time, starting in 12 seconds

    private NavMeshAgent agent;
    private Animator animator;
    private MeshRenderer[] meshRenderers; // Store MeshRenderers to control the display
    private bool isWaiting = false; 
    private float waitTimer = 0f; // timer

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        // Disable movement and animation and wait 12 seconds before enabling it
        agent.enabled = false;
        animator.SetFloat("Speed", 0f); // Disables the initial playback of the animation

        // Start the timer, after 12 seconds move the NPC and start the animation
        StartCoroutine(DelayedStartMovement());
    }

    void Update()
    {
        if (agent.enabled)
        {
            // Update the Speed parameter of the animation
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed); // Set Speed to control the running animation

            // Check whether the target point has been reached
            if (!agent.pathPending && agent.remainingDistance < 0.5f && !isWaiting)
            {
                // Reach the target point, start to wait and disappear
                isWaiting = true;
                waitTimer = waitTimeAtTarget;
                HideNPC(); // Hide NPCS, but keep them active
            }

            // If waiting, count down
            if (isWaiting)
            {
                waitTimer -= Time.deltaTime;
                if (waitTimer <= 0)
                {
                    // Wait for the end, reappear and start from the beginning
                    ReappearAtStart();
                    isWaiting = false;
                }
            }
        }
    }

    // Start moving NPCS and animate them after a delay of 12 seconds
    IEnumerator DelayedStartMovement()
    {
        yield return new WaitForSeconds(startDelay); // Delay 12 seconds
        agent.enabled = true; // Enable NavMeshAgent
        animator.SetFloat("Speed", 1f); // Start the animation
        StartMovement();
    }

    void StartMovement()
    {
        // Set the new target to targetPoint
        agent.SetDestination(targetPoint.position);
        ShowNPC(); // Displays NPC
    }

    void HideNPC()
    {
        // Hide the NPC's visibility, but keep the object active
        foreach (var renderer in meshRenderers)
        {
            renderer.enabled = false;
        }
    }

    void ReappearAtStart()
    {
        // Make the NPC reappear in the starting position
        foreach (var renderer in meshRenderers)
        {
            renderer.enabled = true;
        }
        transform.position = startPoint.position; // Reset to the starting position
        StartMovement(); // Move to the target point again
    }

    void ShowNPC()
    {
        // Displays NPC
        foreach (var renderer in meshRenderers)
        {
            renderer.enabled = true;
        }
    }
}
