using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour // Script attached to the player object
{
    public CharacterController controller; // Reference to the character controller component
    public Transform cam; // Reference to the main camera transform

    public float speed = 6f; // Movement speed
    private float gravity = -9.821f; // Gravity value
    [SerializeField] private float gravityMultiplier = 3.0f; // Gravity multiplier obviously
    private float velocity;

    public float turnSmoothTime = 0.1f; // Smoothness of camera rotation

    float turnSmoothVelocity; // Current camera rotation velocity

    // Calculate the movement direction in world space
    private Vector3 CalculateMovementDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // Get the horizontal axis input (A/D or left/right arrow keys)
        float vertical = Input.GetAxisRaw("Vertical"); // Get the vertical axis input (W/S or up/down arrow keys)

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // Calculate the movement direction
        return direction;
    }

    // Rotate the player towards the target angle
    private void RotatePlayer(float targetAngle)
    {
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    // Move the player in the specified direction
    private void MovePlayer(Vector3 moveDir)
    {
        controller.Move(moveDir.normalized * speed * Time.deltaTime);
    }

    // Apply gravity to the character controller
    private void ApplyGravity()
    {
        controller.Move(new Vector3(0, gravity * gravityMultiplier * Time.deltaTime, 0));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = CalculateMovementDirection();

        if (direction.magnitude >= 0.1f) // If the movement direction is greater than a certain threshold
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            RotatePlayer(targetAngle);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            MovePlayer(moveDir);

            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        ApplyGravity();
    }
}
