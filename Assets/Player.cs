using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    public float playerSpeed = 6.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = transform;
    }

    void FixedUpdate()
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

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        _playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        controller.Move(_playerVelocity * Time.fixedDeltaTime);
    }
}