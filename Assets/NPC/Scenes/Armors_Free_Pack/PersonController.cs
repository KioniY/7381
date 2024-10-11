using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;  // 移动速度
    public float mouseSensitivity = 2.0f;  // 鼠标灵敏度
    public Transform playerCamera;  // 角色的相机

    private float verticalRotation = 0f;  // 垂直方向旋转

    private CharacterController characterController;
    private Animator animator;  // 角色的Animator组件

    void Start()
    {
        // 获取Character Controller和Animator
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // 锁定并隐藏鼠标指针
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 控制水平旋转
        transform.Rotate(Vector3.up * mouseX);

        // 控制垂直旋转
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // 获取键盘输入 (WASD)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // 移动方向
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // 使用Character Controller进行移动
        characterController.Move(move * speed * Time.deltaTime);

        // 计算速度并将其传递给Animator的Speed参数
        float velocity = characterController.velocity.magnitude;
        animator.SetFloat("Speed", velocity);  // 将角色的速度传递给Animator中的Speed参数
    }
}
