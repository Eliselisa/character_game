using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] string actionMapName = "Player";
    [SerializeField] Transform playerTransform;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float lookSpeed = 30f;
    [SerializeField] Animator playerAnimator;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] CapsuleCollider playerCollider;
    [SerializeField] PlayerDimensions standingDimensions;
    [SerializeField] PlayerDimensions crouchedDimensions;

    bool crouched;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lookDirection = Vector3.zero;

    private float groundCheckDistance = 0.2f;
    private float groundedCheckOffset = 0.1f; // Adjust this value based on your player model's height

    [Serializable]
    public struct PlayerDimensions
    {
        public float Height;
        public float Radius;
        public Vector3 Center;
    }

    private void Start()
    {
        playerInput.onActionTriggered += HandleActionTriggered;


        // Lock the cursor to the center of the screen and hide it

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        playerTransform.Rotate(lookDirection * lookSpeed * Time.deltaTime, Space.Self);
        playerTransform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.Self);

        // Update animator parameters based on movement
        playerAnimator.SetFloat("Forward", moveDirection.z);
        playerAnimator.SetFloat("Strafe", moveDirection.x);
        playerAnimator.SetBool("Grounded", IsGrounded());
        
    }
    
    private bool IsGrounded()
    {
        // Check if the player is grounded by casting a ray downwards from the player's position
        if (Physics.Raycast(playerTransform.position + Vector3.up * groundedCheckOffset, Vector3.down, out RaycastHit hit, groundCheckDistance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    void Jump()
    {
        if(!IsGrounded())
        {
            return; // Prevent jumping if not grounded
        }

        var rigidbody = playerTransform.GetComponent<Rigidbody>();

        // Implement jump logic here
        // For example, you can add a vertical force to the player's Rigidbody if it has one
        // If using Rigidbody, you might do something like:
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playerAnimator.SetTrigger("Jump"); // Trigger jump animation
        playerAnimator.SetBool("Grounded", false); 
    }


    private void HandleActionTriggered(InputAction.CallbackContext context)
    // Handles input actions triggered by the player
    {
        if (context.action.actionMap.name == actionMapName)
        {
            switch (context.action.name)
            {
                case "Move":
                    //mover player
                   
                    Vector2 moveInput = context.ReadValue<Vector2>();
                    moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
                    break;
                
                case "Look":
                    //look around

                    Vector2 lookInput = context.ReadValue<Vector2>();
                    lookDirection = new Vector3(0f, lookInput.x, 0f);
                    break;

                case "Jump":
                    if (context.performed)
                    {
                        Jump();
                    }
                    break;

                case "Crouch":
                    if (context.phase == InputActionPhase.Performed) 
                    {
                        ToggleCrouch();
                    }

                    break;


                default:
                    break;
            }
        }
    }

    private void ToggleCrouch()
    {
        if (!crouched)
        {
            playerAnimator.SetBool("Crouched", true);
            crouched = true;

            playerCollider.height = crouchedDimensions.Height;
            playerCollider.radius = crouchedDimensions.Radius;
            playerCollider.center = crouchedDimensions.Center;
        }
        else
        {
            playerAnimator.SetBool("Crouched", false);
            crouched = false;

            playerCollider.height = standingDimensions.Height;
            playerCollider.radius = standingDimensions.Radius;
            playerCollider.center = standingDimensions.Center;
        }
    }
}
