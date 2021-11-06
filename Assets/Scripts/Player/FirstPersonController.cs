using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Characters;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float mouseSens = 10f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float gravityScale = -9.81f;
    [SerializeField] private BaseCharacter character;

    // the shooting accuracy (of the player) is a value between 0 and 1.
    // factors like moving, jumping, crouching, ADS will all improve said accuracy
    // Weapon's also have their own max accuracy which is determined by the type of gun and its attachments
    private float _shootingAccuracy = 0.75f;
    public event Action<bool, bool, float> OnShootEvent;
    
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
        _yRotation -= -_mouseX;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);

        Vector3 motion = transform.right * _xPos + transform.forward * _zPos;
        characterController.Move(motion * (character.MovementSpeed * Time.deltaTime));

        _velocity.y += (character.GravityScaleMultiplier * gravityScale) * Time.deltaTime;
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
        if (character.CanDoubleJump && value.performed && value.interaction is MultiTapInteraction)
        {
            _velocity.y = Mathf.Sqrt(character.JumpHeight * -2f * gravityScale);
            Debug.Log("Double Jump");
        }

        if (characterController.isGrounded && value.started)
        {
            _velocity.y = Mathf.Sqrt(character.JumpHeight * -2f * gravityScale);
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        bool holdingShoot = context.interaction is HoldInteraction && context.performed;
        bool shooting = context.started || context.performed;
        Debug.Log(holdingShoot);
        
        OnShootEvent?.Invoke(shooting, holdingShoot, _shootingAccuracy);
    }
}