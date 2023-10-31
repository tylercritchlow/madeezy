using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    private float turnSmoothVelocity;
    private bool _groundedPlayer;
    
    public float turnSmoothTime = 0.1f;
    
    // The player's current velocity.
    private Vector3 _playerVelocity;
    
    // The height of the player's jump.
    public float jumpHeight = 1.0f;

    // The value of gravity.
    public float gravityValue = -9.81f;
    
    
    // Update is called once per frame
    private void Update()
    {
        _groundedPlayer = controller.isGrounded;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        
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
