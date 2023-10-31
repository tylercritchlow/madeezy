// This script controls the movement of the player character.
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // A reference to the character controller component.
    public CharacterController controller;

    // The player's current velocity.
    private Vector3 _playerVelocity;

    // Whether the player is currently grounded.
    private bool _groundedPlayer;

    // The player's movement speed.
    public float playerSpeed = 6.0f;

    // The height of the player's jump.
    public float jumpHeight = 1.0f;

    // The value of gravity.
    public float gravityValue = -9.81f;

    // A reference to the player's transform component.
    private Transform _playerTransform;

    // Start is called before the first frame update.
    void Start()
    {
        // Get the player's transform component.
        _playerTransform = transform;
    }

    // FixedUpdate is called once per physics frame.
    // This is the best place to handle physics-related code.
    void FixedUpdate()
    {
        // Check if the player is grounded.
        _groundedPlayer = controller.isGrounded;

        // If the player is grounded and their y velocity is negative,
        // set their y velocity to 0 to prevent them from sinking into the ground.
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        // Get the player's movement input.
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        // Calculate the player's speed.
        var speed = playerSpeed * Time.fixedDeltaTime;

        // Move the player.
        controller.Move(move * speed);

        // If the player is moving, rotate them to face the direction they are moving in.
        if (move != Vector3.zero)
        {
            _playerTransform.forward = move;
        }

        // If the player presses the jump button and they are grounded,
        // give them an upward velocity.
        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // Apply gravity to the player.
        _playerVelocity.y += gravityValue * Time.fixedDeltaTime;

        // Move the player again, this time taking gravity into account.
        controller.Move(_playerVelocity * Time.fixedDeltaTime);
    }
}