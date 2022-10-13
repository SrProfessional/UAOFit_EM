using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInputActions playerInput;
    public CharacterController controller;
    public Transform transformPlayer;
    public float rightJoystickSensibility;
    float yRotation = 0f;
    Vector2 movementInput, rotationInput;
    Vector3 move, rotate;

    void Awake()
    {
        playerInput = new PlayerInputActions();
    }

    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    void FixedUpdate()
    {
        movementInput = playerInput.Player.Move.ReadValue<Vector2>();
        rotationInput = playerInput.Player.Rotation.ReadValue<Vector2>();

        move = new Vector3(movementInput.y, 0f, -movementInput.x);

        if (movementInput != Vector2.zero)
        {
            Vector3 displacement = (transformPlayer.right * movementInput.x + transformPlayer.forward * movementInput.y);
            //controller.Move(move * Time.deltaTime);
            controller.Move(displacement * Time.deltaTime);
        }

        if(rotationInput != Vector2.zero)
        {
            transformPlayer.Rotate(Vector3.forward, rotationInput.y * Time.deltaTime * rightJoystickSensibility);

            yRotation -= rotationInput.y * .1f;
            yRotation = Mathf.Clamp(yRotation, -40f, 60f);
            Vector3 targetRotation = transformPlayer.eulerAngles;
            targetRotation.x = yRotation;
            transformPlayer.eulerAngles = targetRotation;
        }
    }
}