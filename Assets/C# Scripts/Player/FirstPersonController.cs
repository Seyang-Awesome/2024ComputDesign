using System.Collections;
using UnityEngine;

public class FirstPersonController : MonoSingleton<FirstPersonController>
{
    protected override bool IsDontDestroyOnLoad => false;
    
    [SerializeField] private float jumpSpeed = 10.0f; // 跳跃速度
    [SerializeField] private float stepSeCheckInterval = 0.5f;
    
    public float rotationSpeed = 5.0f;
    public float moveSpeed = 5.0f;
    public float pitchRange = 80.0f;
    public Vector3 detectDir = Vector3.down;
    public float detectLength = 2.2f;
    public AudioRandomGroup audioRandomGroup;

    private Rigidbody rb;
    private float pitch = 0.0f;

    public bool IsCanControl { get; set; } = true;
    private void Start()
    {
        Inputs.GetInstance().SetInputEnable(true);
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(StepSeCheck());
    }

    void Update()
    {
        if (!IsCanControl) return;
        
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

        if (mouseX != 0f || mouseY != 0f)
        {
            
        }
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

    public void SetMouseCursor(CursorLockMode mode)
    {
        Cursor.lockState = mode;
    }

    private IEnumerator StepSeCheck()
    {
        while (true)
        {
            if (Inputs.GetInstance().MoveX != 0f || Inputs.GetInstance().MoveY != 0f)
            {
                AudioManager.Instance.PlaySe(audioRandomGroup.GetRandomClip());
            }

            yield return new WaitForSeconds(stepSeCheckInterval);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, detectDir * detectLength);
    }
}