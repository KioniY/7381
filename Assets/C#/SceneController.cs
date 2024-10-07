using UnityEngine;
using UnityEngine.UI; // For UI components
using UnityEngine.Video;
using System.Collections; // For IEnumerator and Coroutines

public class SceneController : MonoBehaviour
{
    public GameObject whiteboard; // UI Panel for the whiteboard
    public RawImage rawImage; // Raw Image to display the video/animation
    public VideoPlayer videoPlayer; // Video Player for handling video playback
    public Transform cameraTransform; // Camera's Transform

    void Start()
    {
        // Show the whiteboard and start playing the video
        ShowWhiteboard();

        // Listen for the video end event
        videoPlayer.loopPointReached += OnVideoEnd;

        // Start the video
        videoPlayer.Play();
    }

    void ShowWhiteboard()
    {
        // Ensure the whiteboard is visible
        whiteboard.SetActive(true);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // When the video ends, start fading out the whiteboard and the raw image
        StartCoroutine(FadeOutWhiteboardAndRawImage());
    }

    IEnumerator FadeOutWhiteboardAndRawImage()
    {
        // Get the Image component of the whiteboard
        Image whiteboardImage = whiteboard.GetComponent<Image>();
        Color whiteboardColor = whiteboardImage.color;

        // Get the color of the Raw Image (used for video/animation)
        Color rawImageColor = rawImage.color;

        // Fade out both the whiteboard and the Raw Image over 2 seconds
        for (float t = 0; t < 1; t += Time.deltaTime / 2f)
        {
            // Fade out the whiteboard
            whiteboardColor.a = Mathf.Lerp(1, 0, t);
            whiteboardImage.color = whiteboardColor;

            // Fade out the Raw Image (animation)
            rawImageColor.a = Mathf.Lerp(1, 0, t);
            rawImage.color = rawImageColor;

            yield return null; // Wait for the next frame
        }

        // After fully fading out, hide the whiteboard and the raw image
        whiteboard.SetActive(false);
        rawImage.gameObject.SetActive(false);

        // Start the camera movement and rotation
        MoveAndRotateCamera();
    }

    void MoveAndRotateCamera()
    {
        // Start the coroutine for camera movement and rotation
        StartCoroutine(RiseAndRotateCamera());
    }

    IEnumerator RiseAndRotateCamera()
    {
        Vector3 originalPosition = cameraTransform.position;
        Vector3 risePosition = originalPosition + new Vector3(0, 6, 0); // Move the camera up by 6 units

        Vector3 originalRotation = cameraTransform.eulerAngles; // Use Euler angles for rotation
        Vector3 targetRotation = originalRotation + new Vector3(0, 360, 0); // Rotate 360 degrees

        float riseDuration = 2f; // Time taken to rise
        float rotateDuration = 5f; // Time taken to rotate
        float descendDuration = 2f; // Time taken to descend

        // 1. Camera rising phase (2 seconds)
        float timeElapsed = 0f;
        while (timeElapsed < riseDuration)
        {
            float progress = timeElapsed / riseDuration; // Progress percentage
            cameraTransform.position = Vector3.Lerp(originalPosition, risePosition, progress);
            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        // Ensure the camera is at the final risen position
        cameraTransform.position = risePosition;

        // 2. Camera rotation phase (3 seconds)
        timeElapsed = 0f;
        while (timeElapsed < rotateDuration)
        {
            float progress = timeElapsed / rotateDuration;
            // Use Vector3 to linearly interpolate rotation
            cameraTransform.eulerAngles = Vector3.Lerp(originalRotation, targetRotation, progress);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        // Ensure the camera completes its rotation
        cameraTransform.eulerAngles = targetRotation;

        // Pause for 1 second before descending
        yield return new WaitForSeconds(1f);

        // 3. Camera descending phase (2 seconds)
        timeElapsed = 0f;
        while (timeElapsed < descendDuration)
        {
            float progress = timeElapsed / descendDuration;
            cameraTransform.position = Vector3.Lerp(risePosition, originalPosition, progress);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        // Ensure the camera returns to its original position
        cameraTransform.position = originalPosition;
        cameraTransform.eulerAngles = originalRotation;
    }
}
