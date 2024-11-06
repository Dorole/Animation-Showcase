using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController_1DBlend : MonoBehaviour
{
    [SerializeField] private float _velocityChangeRate = 10;
    [Tooltip("Move speed has to match the value in the blend tree.")]
    [SerializeField] private float _moveSpeed = 2;
    [Tooltip("Run speed has to match the value in the blend tree.")]
    [SerializeField] private float _runSpeed = 6;

    private Animator _animator;
    private float _velocity = 0;
    private float _targetVelocity = 0;

    private int _animIDVelocity;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        AssignAnimationIDs();
    }

    private void Update()
    {
        BlendAnimation_1D();
    }

    private void BlendAnimation_1D()
    {
        bool fwdPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        _targetVelocity = fwdPressed ? (runPressed ? _runSpeed : _moveSpeed) : 0;

        _velocity = Mathf.Lerp(_velocity, _targetVelocity, Time.deltaTime * _velocityChangeRate);

        _animator.SetFloat(_animIDVelocity, _velocity);
    }

    private void AssignAnimationIDs()
    {
        _animIDVelocity = Animator.StringToHash("Velocity");
    }
}
