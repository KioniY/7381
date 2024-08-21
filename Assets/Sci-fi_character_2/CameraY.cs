using UnityEngine;

public class CameraY : MonoBehaviour
{
    // left, right control
    public Transform y_Axis;
    // up, down control
    public Transform x_Axis;
    // z
    public Transform z_Axis;
    // near, far
    public Transform zoom_Axis;
    
    // player
    public Transform player;
    
    // round speed 旋转速度
    public float roSpeed = 180;
    // scale speed 缩放速度
    public float scSpeed = 50;
    // limit angle
    public float limitAngle = 45;
    
    // mouse
    private float hor, ver, scrollView;
    private float x = 0, sc = 10;
    
    // follow the player
    public bool followFlag;
    // turn player
    public bool turnFlag;

    // smooth follow
    private Vector3 initialOffset; 
 

    private void Start()
    {
        x = x_Axis.localEulerAngles.x; 
        
        // position between camera and player
        if (player != null)
        {
            initialOffset = y_Axis.position - player.position;
            
            // initialize camera's position
            y_Axis.position = player.position + initialOffset;
        }
    }

    private void Update()
    {
        hor = Input.GetAxis("Mouse X");
        ver = Input.GetAxis("Mouse Y");
        scrollView = Input.GetAxis("Mouse ScrollWheel");

        if (hor != 0)
        {
            y_Axis.Rotate(Vector3.up * roSpeed * hor * Time.deltaTime);
        }

        if (ver != 0)
        {
            x -= ver * Time.deltaTime * roSpeed;
            x = Mathf.Clamp(x, -limitAngle, limitAngle);
            x_Axis.localRotation = Quaternion.Euler(x, 0, 0); 
        }

        // near, far
        if (scrollView != 0)
        {
            sc -= scrollView * scSpeed;
            sc = Mathf.Clamp(sc, 3, 10);
            zoom_Axis.localPosition = new Vector3(0, 0, -sc);
        }
    }

    private void LateUpdate()
    {
        // follow player
        if (followFlag && player != null)
        {
            Vector3 targetPosition = player.position + initialOffset; 
            y_Axis.position = Vector3.Lerp(y_Axis.position, targetPosition, Time.deltaTime * 5f); 
        }

        // turn player
        if (turnFlag && player != null)
        {
            Vector3 cameraForward = new Vector3(y_Axis.forward.x, 0, y_Axis.forward.z).normalized;
            player.forward = cameraForward;
        }

        // debug
        Debug.Log($"x_Axis Rotation: {x_Axis.localEulerAngles}");
        Debug.Log($"Player Forward: {player.forward}");
        Debug.Log($"Camera Position: {y_Axis.position}");
    }
}

