using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float mouseSens = 10f;

    [SerializeField] private Transform cameraTransform;

    [SerializeField] private CharacterController characterController;

    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float jumpHeight = 2f;

    [SerializeField] private float gravityScale = -9.81f;
    [SerializeField] private float gravityScaleMultiplier = 1.5f;

    private float _mouseX;
    private float _mouseY;
    private float _xRotation = 0f;
    private float _yRotation = 0f;

    private float _xPos;
    private float _zPos;


    private Vector3 _velocity;

    private void Start()
    {
        if (characterController == null) characterController = GetComponent<CharacterController>();

        //lock cursor to mid
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _xRotation -= _mouseY;
        _yRotation -= _mouseX;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0, 0f);

        transform.Rotate(Vector3.up * _mouseX);

        Vector3 motion = transform.right * _xPos + transform.forward * _zPos;
        characterController.Move(motion * (moveSpeed * Time.deltaTime));

        _velocity.y += (gravityScaleMultiplier * gravityScale) * Time.deltaTime;
        characterController.Move(_velocity * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 direction = value.ReadValue<Vector2>();
        _xPos = direction.x;
        _zPos = direction.y;
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        Vector2 direction = value.ReadValue<Vector2>();
        _mouseX = direction.x * mouseSens * Time.deltaTime;
        _mouseY = direction.y * mouseSens * Time.deltaTime;
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.performed && value.interaction is MultiTapInteraction)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
            Debug.Log("Double Jump");
        }

        if (value.started && characterController.isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
        }
    }
}