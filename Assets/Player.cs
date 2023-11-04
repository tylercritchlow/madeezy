// This script controls the movement of the player character.
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    private Vector3 _playerVelocity;

    private bool _groundedPlayer;

    // The player's movement speed.
    public float playerSpeed = 6.0f;

    public float jumpHeight = 1.0f;

    public float gravityValue = -9.81f;

    private Transform _playerTransform;

    void Start()
    {
        _playerTransform = transform;
    }

    void Update()
    {
        _groundedPlayer = controller.isGrounded;


        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        var speed = playerSpeed * Time.fixedDeltaTime;

        controller.Move(move * speed);

        if (move != Vector3.zero)
        {
            _playerTransform.forward = move;
        }

        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            Debug.Log("grounded player");
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * gravityValue);
        }

        _playerVelocity.y += gravityValue * Time.deltaTime;

        controller.Move(_playerVelocity * Time.deltaTime);
    }
}