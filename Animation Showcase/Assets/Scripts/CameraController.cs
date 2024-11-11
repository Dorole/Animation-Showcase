using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    [SerializeField] private Transform _cinemachineCameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    [SerializeField] private float _topClamp = 30.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    [SerializeField] private float _bottomClamp = -15.0f;

    [Tooltip("Additional degress to override the camera; for fine tuning camera position when locked")]
    [SerializeField] private float _cameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axis")]
    [SerializeField] private bool _lockCameraPosition = false;

    [SerializeField] private UserInput _input;

    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;

    private void Start()
    {
        if (_input == null)
            _input = FindObjectOfType<UserInput>();

        _cinemachineTargetYaw = _cinemachineCameraTarget.rotation.eulerAngles.y;
    }

    private void LateUpdate()
    {
        if (!_input.CameraLookLocked)
            CameraRotation();
    }

    private void CameraRotation()
    {
        if (_input.Look.sqrMagnitude >= _threshold && !_lockCameraPosition)
        {
            //if input is not provided by mouse, then multiply by Time.deltaTime
            _cinemachineTargetYaw += _input.Look.x;
            _cinemachineTargetPitch += _input.Look.y;
        }

        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);
        _cinemachineCameraTarget.rotation = Quaternion.Euler(_cinemachineTargetPitch + _cameraAngleOverride, _cinemachineTargetYaw, 0);
    }

    private float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f)
            lfAngle += 360f;

        if (lfAngle > 360f)
            lfAngle -= 360f;

        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
