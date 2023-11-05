using UnityEngine;
using UnityEngine.InputSystem;

public class Testing : MonoBehaviour
{
    public CharacterController controller;
    public PlayerInput playerInput;
    public Transform cam;
    private float _turnSmoothVelocity;
    private readonly float _turnSmoothTime = 0.1f;
    private bool _groundedPlayer;
    
    // All movement variables
    public float speed = 6f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    private Vector3 _playerVelocity;
    


    private void Update()
    {
        _groundedPlayer = controller.isGrounded;
        
        // Don't delete this, it breaks the jump
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        var horizontal =  Input.GetAxisRaw("Horizontal");
        var vertical =  Input.GetAxisRaw("Vertical");
        var direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * (speed * Time.deltaTime));
        }
        
        
        // If the player presses the jump button and they are grounded, give them an upward velocity.
        if (playerInput.actions["Jump"].IsPressed() && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // Apply gravity to the player.
        _playerVelocity.y += gravityValue * Time.deltaTime;

        // Move the player again, this time taking gravity into account.
        controller.Move(_playerVelocity * Time.deltaTime);
    }
}