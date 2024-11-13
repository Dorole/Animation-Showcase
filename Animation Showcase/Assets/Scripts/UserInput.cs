using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class UserInput : MonoBehaviour
{
    public static event Action<Vector2> OnInteractPressed;

    [SerializeField] private float _unlockCameraDelay = 0.5f;

    private PlayerControls _playerInput;
    private PlayerControls.CharacterControlsActions _characterControls;
    private bool _cameraLookLocked = true;

    public bool CameraLookLocked => _cameraLookLocked;
    public Vector2 CurrentMovement { get; private set; }
    public Vector2 Look { get; private set; }
    public Vector2 CurrentPointerPosition { get; private set; }
    public bool WalkPressed { get; private set; }
    public bool RunPressed { get; private set; }
    //public bool InteractPressed { get; private set; }


    private void Awake()
    {
        _playerInput = new PlayerControls();
        _characterControls = _playerInput.CharacterControls;

        _characterControls.Run.performed += ctx => RunPressed = ctx.ReadValueAsButton();
        
        _characterControls.Walk.performed += Walk_performed;
        _characterControls.Walk.canceled += Walk_canceled;

        _characterControls.Interact.performed += Interact_performed;
    }

    private void Start()
    {
        StartCoroutine(UnlockCamInputAfterDelay());
    }

    private IEnumerator UnlockCamInputAfterDelay()
    {
        yield return new WaitForSeconds(_unlockCameraDelay);

        _characterControls.Look.performed += ctx => Look = ctx.ReadValue<Vector2>();
        _cameraLookLocked = false;
    }

    private void Walk_performed(InputAction.CallbackContext ctx)
    {
        CurrentMovement = ctx.ReadValue<Vector2>();
        WalkPressed = CurrentMovement.x != 0 || CurrentMovement.y != 0;
    }

    private void Walk_canceled(InputAction.CallbackContext ctx)
    {
        CurrentMovement = Vector2.zero;
        WalkPressed = false;
    }

    private void Interact_performed(InputAction.CallbackContext ctx)
    {
        OnInteractPressed?.Invoke(ReadPointerPosition());
    }

    private Vector2 ReadPointerPosition()
    {
        return _characterControls.PointerPosition.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }
}
