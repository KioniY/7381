using UnityEngine;
using System.Collections;

public class AudioAndCameraController : MonoBehaviour
{
    public Transform cameraTransform; // Camera's Transform
    public Transform targetPosition; // The target object the camera will move to
    public AudioSource audioSource; // Audio source for playing narration
    public AudioClip firstAudioClip; // First audio clip to play
    public AudioClip secondAudioClip; // Second audio clip to play
    private Vector3 lastCameraPosition; // Used to track the camera's last position
    private bool hasPlayedFirstAudio = false; // Ensure first audio plays only once
    private bool hasPlayedSecondAudio = false; // Ensure second audio plays only once
    private bool isCameraMoving = false; // Used to track if the camera is moving towards the target
    private Vector3 originalCameraPosition; // Store the original camera position

    public float moveDuration = 3f; // Duration for the camera to move towards the target

    void Start()
    {
        // Store the initial camera position
        lastCameraPosition = cameraTransform.position;
        originalCameraPosition = cameraTransform.position; // Store the original position for later
    }

    void Update()
    {
        // Detect player movement by checking if the camera's position has changed
        if (cameraTransform.position != lastCameraPosition)
        {
            // If the player has moved, check and play the first audio clip
            if (!hasPlayedFirstAudio && !isCameraMoving)
            {
                PlayAudio(firstAudioClip);
                hasPlayedFirstAudio = true; // Ensure it only plays once
            }

            // Move the camera after the first audio has finished and start the second audio
            else if (!hasPlayedSecondAudio && !audioSource.isPlaying && !isCameraMoving)
            {
                StartCoroutine(MoveCameraToTargetAndPlaySecondAudio());
                hasPlayedSecondAudio = true; // Ensure second audio logic only happens once
            }
        }

        // Update last camera position for the next frame
        lastCameraPosition = cameraTransform.position;
    }

    // Play audio
    void PlayAudio(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    // Move the camera to the target position and play the second audio
    IEnumerator MoveCameraToTargetAndPlaySecondAudio()
    {
        isCameraMoving = true;

        Vector3 originalPosition = cameraTransform.position;
        float timeElapsed = 0f;

        // Smoothly move the camera to the target position
        while (timeElapsed < moveDuration)
        {
            cameraTransform.position = Vector3.Lerp(originalPosition, targetPosition.position, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the camera reaches the target position
        cameraTransform.position = targetPosition.position;

        // Play the second audio clip
        PlayAudio(secondAudioClip);

        // Wait for the second audio to finish playing
        yield return new WaitForSeconds(secondAudioClip.length);

        // After the second audio finishes, move the camera back to its original position
        StartCoroutine(MoveCameraBackToOriginalPosition());
    }

    // Move the camera back to the original position
    IEnumerator MoveCameraBackToOriginalPosition()
    {
        Vector3 currentPosition = cameraTransform.position;
        float timeElapsed = 0f;

        // Smoothly move the camera back to the original position
        while (timeElapsed < moveDuration)
        {
            cameraTransform.position = Vector3.Lerp(currentPosition, originalCameraPosition, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the camera is back at the original position
        cameraTransform.position = originalCameraPosition;
        isCameraMoving = false; // Allow future movement detection if needed
    }
}
