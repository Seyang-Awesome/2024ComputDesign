using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField]
    private float jumpSpeed = 10.0f; // 跳跃速度
    public float rotationSpeed = 5.0f;
    public float moveSpeed = 5.0f;
    public float pitchRange = 80.0f;
    public Vector3 detectDir = Vector3.down;
    public float detectLength = 2.2f;

    private Rigidbody rb;
    private float pitch = 0.0f;


    private void Start()
    {
        Inputs.GetInstance().SetInputEnable(true);
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        float mouseX = Inputs.GetInstance().MouseX * rotationSpeed;
        float mouseY = -Inputs.GetInstance().MouseY * rotationSpeed;

        transform.Rotate(0.0f, mouseX, 0.0f);

        pitch += mouseY;
        pitch = Mathf.Clamp(pitch, -pitchRange, pitchRange);
        Camera.main.transform.localRotation = Quaternion.Euler(pitch, 0.0f, 0.0f);

        float moveX = Inputs.GetInstance().MoveX * moveSpeed;
        float moveZ = Inputs.GetInstance().MoveY * moveSpeed;

        Vector3 moveDirection = Camera.main.transform.forward * moveZ;
        moveDirection += Camera.main.transform.right * moveX;
        moveDirection.y = 0.0f;

        moveDirection.Normalize();
        moveDirection *= moveSpeed;

        rb.velocity = moveDirection + Vector3.up * rb.velocity.y;
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit[] hits = Physics.RaycastAll(transform.position, detectDir, detectLength);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.tag != "Player")
                {
                    Jump();
                    break; // 找到一个符合条件的物体就跳跃一次，然后跳出循环
                }
            }
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x,jumpSpeed,rb.velocity.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, detectDir * detectLength);
    }
}