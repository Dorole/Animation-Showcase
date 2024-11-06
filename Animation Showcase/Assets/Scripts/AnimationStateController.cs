using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    [SerializeField] private float _velocityChangeRate = 10;
    [Tooltip("Move speed has to match the value in the blend tree.")]
    [SerializeField] private float _moveSpeed = 0.5f;
    [Tooltip("Run speed has to match the value in the blend tree.")]
    [SerializeField] private float _runSpeed = 2;

    private Animator _animator;
    private float _velocityZ = 0;
    private float _velocityX = 0;

    private int _animIDVelocityX;
    private int _animIDVelocityZ;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        AssignAnimationIDs();
    }

    private void Update()
    {
        BlendAnimation_2D_Strafe();
    }

    private void BlendAnimation_2D_Strafe()
    {
        //switch to new input
        bool fwdPressed = Input.GetKey(KeyCode.W);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool leftPressed = Input.GetKey(KeyCode.A); //use axis instead
        bool rightPressed = Input.GetKey(KeyCode.D);

        float targetVelocityZ = fwdPressed ? (runPressed ? _runSpeed : _moveSpeed) : 0;
        float targetVelocityX = (leftPressed || rightPressed) ? (runPressed ? _runSpeed : _moveSpeed) : 0;

        if (targetVelocityX != 0)
        {
            float direction = leftPressed ? -1 : 1;
            targetVelocityX *= direction;
        }

        _velocityZ = Mathf.Lerp(_velocityZ, targetVelocityZ, Time.deltaTime * _velocityChangeRate);
        _velocityX = Mathf.Lerp(_velocityX, targetVelocityX, Time.deltaTime * _velocityChangeRate);

        _animator.SetFloat(_animIDVelocityZ, _velocityZ);
        _animator.SetFloat(_animIDVelocityX, _velocityX);
    }

    private void AssignAnimationIDs()
    {
        _animIDVelocityX = Animator.StringToHash("VelocityX");
        _animIDVelocityZ = Animator.StringToHash("VelocityZ");
    }

}
