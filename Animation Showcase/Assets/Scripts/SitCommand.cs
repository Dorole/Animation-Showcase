using UnityEngine;

public class SitCommand : IInteractCommand
{
    private CharacterMovementController _player;
    private Transform _interactionPoint;
    private Animator _animator;
    private int _animIDSit;

    public SitCommand(CharacterMovementController player, Animator animator, Transform interactionPoint)
    {
        _player = player;
        _animator = animator;
        _interactionPoint = interactionPoint;

        _animIDSit = Animator.StringToHash("Sit");
    }


    public void MoveToInteractionPoint()
    {
        _player.AutoMove(_interactionPoint);
        _player.OnAutoMoveComplete += TriggerAnimation;
    }

    public void TriggerAnimation()
    {
        _animator.SetBool(_animIDSit, true);
        _player.OnAutoMoveComplete -= TriggerAnimation;
    }
}
