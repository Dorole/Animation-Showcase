using UnityEngine;

public class SitCommand : BaseCommand<EInteractionType>
{
    private int _animIDSit = Animator.StringToHash("Sit"); 

    public SitCommand(EInteractionType interaction, Animator animator) : base(interaction, animator)
    {
        Debug.Log("SIT COMMAND CREATED");
    }

    public override bool RequiresFinishPoint() => true;
    public override bool RequiresAutoMove() => true;

    public override void Execute()
    {
        _animator.SetTrigger(_animIDSit);
    }

    public override void Exit() 
    {
        _animator.SetBool(_animIDSit, false);
    }
}
