using UnityEngine;

[RequireComponent(typeof(UserInput))]
public class CharacterMovement : MonoBehaviour
{
    #region FIELDS
    [Header("Character")]
    [SerializeField] private float _velocityChangeRate = 10;

    [Tooltip("Move speed has to match the value in the blend tree.")]
    [SerializeField] private float _moveSpeed = 2;

    [Tooltip("Run speed has to match the value in the blend tree.")]
    [SerializeField] private float _runSpeed = 6;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    [SerializeField] private float _rotationSmoothTime = 0.12f;

    private UserInput _input;
    private Animator _animator;
    private GameObject _mainCamera;
    private int _animIDVelocity;

    //movement
    private float _velocity = 0;
    private float _targetVelocity = 0;
    private float _rotationVelocity; //ref for rotation smoothing
    #endregion

    //TO-DO: Check if current input is mouse!
    //For this you need the action asset component!!

    private void Awake()
    {
        _mainCamera = Camera.main.gameObject;
    }

    private void Start()
    {
        _input = GetComponent<UserInput>();
        _animator = GetComponent<Animator>();
        AssignAnimationIDs();

    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void AssignAnimationIDs()
    {
        _animIDVelocity = Animator.StringToHash("Velocity");
    }

    private void HandleMovement()
    {
        _targetVelocity = _input.WalkPressed ? (_input.RunPressed ? _runSpeed : _moveSpeed) : 0;
        _velocity = Mathf.Lerp(_velocity, _targetVelocity, Time.deltaTime * _velocityChangeRate);
        _animator.SetFloat(_animIDVelocity, _velocity);
    }

    private void HandleRotation()
    {
        Vector3 inputDirection = new Vector3(_input.CurrentMovement.x, 0.0f, _input.CurrentMovement.y).normalized;

        if (inputDirection != Vector3.zero)
        {
            float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity, _rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
    }
}
