using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private Animator _animator;
    private int _animIDSitting;
    private bool _isSitting = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animIDSitting = Animator.StringToHash("Sit");
    }

    public void Sit(Transform interactionPoint)
    {
        Debug.Log("SIT");

        GetComponent<CharacterMovementController>().AutoMove(interactionPoint);

        //_animator.SetTrigger(_animIDSitting);
    }


    // ***** ANIMATION EVENTS *****
    //Placed at the end of sit-down and stand-up animations
    public void ToggleSitting()
    {
        _isSitting = !_isSitting;
    }
}
