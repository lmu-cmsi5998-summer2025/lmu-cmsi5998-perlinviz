using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float smoothTime = 0.1f;

    private Vector3 currentVelocity;
    private float rotationX = 0f;
    private float rotationY = 0f;

    private void Start()
    {
        // Get initial rotation
        Vector3 rotation = transform.eulerAngles;
        rotationX = rotation.y;
        rotationY = rotation.x;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        // Get input axes
        float horizontal = Input.GetAxis("Horizontal"); // A and D
        float vertical = Input.GetAxis("Vertical");     // W and S

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        
        // Transform direction based on camera's orientation
        moveDirection = transform.TransformDirection(moveDirection);
        
        // Apply movement with smoothing
        Vector3 targetPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }

    private void HandleRotation()
    {
        // Only rotate when right mouse button is held down
        if (Input.GetMouseButton(1)) // 1 is the right mouse button
        {
            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Calculate rotation
            rotationX += mouseX;
            rotationY -= mouseY;
            rotationY = Mathf.Clamp(rotationY, -90f, 90f); // Limit vertical rotation

            // Apply rotation
            transform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);
        }
    }
} 