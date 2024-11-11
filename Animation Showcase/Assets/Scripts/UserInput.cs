using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    [SerializeField] private float _unlockCameraDelay = 0.5f;

    private PlayerControls _playerInput;
    private PlayerControls.CharacterControlsActions _characterControls;
    private bool _cameraLookLocked = true;

    public bool CameraLookLocked => _cameraLookLocked;
    public Vector2 CurrentMovement { get; private set; }
    public Vector2 Look { get; private set; }
    public bool WalkPressed { get; private set; }
    public bool RunPressed { get; private set; }


    private void Awake()
    {
        _playerInput = new PlayerControls();
        _characterControls = _playerInput.CharacterControls;

        _characterControls.Run.performed += ctx => RunPressed = ctx.ReadValueAsButton();
        
        _characterControls.Walk.performed += Walk_performed;
        _characterControls.Walk.canceled += Walk_canceled;
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

    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }
}
