using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UserInput))]
public class CharacterMovementController : MonoBehaviour
{
    #region FIELDS
    public event Action OnAutoMoveComplete;

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
    private NavMeshAgent _navAgent;
    private GameObject _mainCamera;
    private int _animIDVelocity;

    private float _velocity = 0;
    private float _targetVelocity = 0;
    private float _rotationVelocity; //ref for rotation smoothing
    private bool _loopAnimation = false;

    private bool _canMove = true;
    private bool _isAutoMoving = false;
    private Vector3 _autoMoveDestination;
    private Transform _autoMoveTarget;
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

        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.enabled = false;

        CharacterStateManager.OnStateChanged += ToggleCanMove;
    }

    private void Update()
    {
        if (_loopAnimation) return;

        if (!_isAutoMoving)
        {
            HandleMovement();
            HandleRotation();
        }
        else
            HandleAutoMovement();      
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
            SmoothRotateOverTime(inputDirection, _mainCamera.transform.eulerAngles.y);
    }

    private void HandleAutoMovement()
    {
        transform.position = new Vector3(_navAgent.nextPosition.x, _autoMoveDestination.y, _navAgent.nextPosition.z); //temporary fix 
        _animator.SetFloat(_animIDVelocity, _moveSpeed);

        if (_navAgent.remainingDistance <= _navAgent.stoppingDistance)
        {
            _isAutoMoving = false;
            ToggleMovementControl(_isAutoMoving);
            StartCoroutine(CO_RotateToTargetForward());
            return;
        }
    }

    private IEnumerator CO_RotateToTargetForward()
    {
        Vector3 targetDirection = _autoMoveTarget.forward;

        while (Vector3.Angle(transform.forward, targetDirection) > 0.1f)
        {
            SmoothRotateOverTime(targetDirection, 0f);
            yield return null;
        }

        _autoMoveTarget = null;
        OnAutoMoveComplete?.Invoke();
    }

    private void SmoothRotateOverTime(Vector3 targetDirection, float cameraY)
    {
        float targetRotation = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg + cameraY;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity, _rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }

    private void ToggleMovementControl(bool isAuto)
    {
        _navAgent.enabled = isAuto;
        _animator.applyRootMotion = !isAuto;
    }

    private void ToggleCanMove(ECharacterState state)
    {
        _canMove = state == ECharacterState.STANDING;
    }

    public void AutoMove(Transform target)
    {
        _autoMoveTarget = target;
        _isAutoMoving = true;
        ToggleMovementControl(_isAutoMoving);

        _autoMoveDestination = new Vector3(target.position.x, transform.position.y, target.position.z);
        _navAgent.SetDestination(_autoMoveDestination);
    }

    
    // ***** ANIMATION EVENTS *****
    /// <summary>
    /// Place at the end of transition animations, for example sit-down and stand-up
    /// </summary>
    public void StartEndLoop()
    {
        _loopAnimation = !_loopAnimation;
    }
}
