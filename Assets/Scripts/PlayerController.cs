using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] string actionMapName = "Player";
    [SerializeField] string uiMapName = "UI";
    [SerializeField] Transform playerTransform;
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float lookSpeed = 30f;
    [SerializeField] Animator playerAnimator;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] CapsuleCollider playerCollider;
    [SerializeField] PlayerDimensions standingDimensions;
    [SerializeField] PlayerDimensions crouchedDimensions;
    [SerializeField] WeaponSelectionUI weaponSelectionUI;
    [SerializeField] Attacker attacker;

    private WeaponController weaponController;

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

        weaponController = playerTransform.GetComponent<WeaponController>();

        weaponSelectionUI.UIStateChanged.AddListener((isOpen) =>
        {
            if (isOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerInput.SwitchCurrentActionMap(uiMapName);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerInput.SwitchCurrentActionMap(actionMapName);
            }
        });
    }

    private void OnDestroy()
    {
        // Unsubscribe from the action triggered event to avoid memory leaks
        playerInput.onActionTriggered -= HandleActionTriggered;
    }

    private void FixedUpdate()
    {
        playerTransform.Rotate(lookDirection * lookSpeed * Time.fixedDeltaTime, Space.Self);
        playerTransform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime, Space.Self);

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

                case "NextWeapon":
                    if (context.performed)
                    {
                        weaponController.NextWeapon();
                    }
                    break;

                case "PreviousWeapon":
                    if (context.performed)
                    {
                        weaponController.PreviousWeapon();
                    }
                    break;

                case "OpenWeaponsMenu":
                    if (context.phase == InputActionPhase.Performed)
                    {
                        if (weaponSelectionUI.IsOpen)
                        {
                            weaponSelectionUI.Close();
                            Cursor.lockState = CursorLockMode.Locked;
                            Cursor.visible = false;
                        }
                        else
                        {
                            weaponSelectionUI.Open();
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                        }

                    }
                    break;

                    case "Attack":
                        if (context.phase == InputActionPhase.Performed)
                    {
                       Debug.Log("Attack action performed");
                        var attackCommand = attacker.CreateAttackCommand();
                        CommandController.Instance.ExecuteCommand(attackCommand);
                    }
                    break;

                default:
                    break;
            }
        }
        else if (context.action.actionMap.name == uiMapName)
        {
            switch (context.action.name)
            {
                case "Cancel":
                    if (context.phase == InputActionPhase.Performed)
                    {
                        weaponSelectionUI.Close();
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                    }
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
