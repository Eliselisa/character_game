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

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lookDirection = Vector3.zero;

    private void Start()
    {
        playerInput.onActionTriggered += HandleActionTriggered;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        playerTransform.Rotate(lookDirection * lookSpeed * Time.deltaTime, Space.Self);
        playerTransform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.Self);
    }

    private void HandleActionTriggered(InputAction.CallbackContext context)
    {
        if(context.action.actionMap.name == actionMapName)
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


                default:
                    break;
            }
        }
    }
}
