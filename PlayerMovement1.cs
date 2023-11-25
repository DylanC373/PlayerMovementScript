using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public float RotationSpeed;
    public Transform CameraTransform;
    public Vector3 CameraOffset;

    private CharacterController CharacterController;

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Player Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = CameraTransform.forward;
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = CameraTransform.right;
        right.y = 0;
        right = right.normalized;

        Vector3 movementDirection = (horizontalInput * right + verticalInput * forward).normalized;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movementDirection += Vector3.up;
        }

        // Move the character based on the movement direction relative to the camera
        MoveCharacter(movementDirection);

        // Jumping
        if (CharacterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            CharacterController.Move(Vector3.up * JumpForce * Time.deltaTime);
        }

        // Camera Rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * mouseX * RotationSpeed * Time.deltaTime);
        CameraTransform.Rotate(Vector3.left * mouseY * RotationSpeed * Time.deltaTime);

        CameraTransform.position = transform.position + CameraOffset;
    }

    // Move the character in the calculated direction
    void MoveCharacter(Vector3 direction)
    {
        float magnitude = Mathf.Clamp01(direction.magnitude) * Speed;
        direction.Normalize();
        CharacterController.SimpleMove(direction * magnitude);
    }
}
