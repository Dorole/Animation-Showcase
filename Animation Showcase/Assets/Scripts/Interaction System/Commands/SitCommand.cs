using UnityEngine;

public class SitCommand : BaseCommand<EInteractionType>
{
    private int _animIDSit = Animator.StringToHash("Sit"); // ovo postaviti izvana??

    public SitCommand(EInteractionType interaction, Animator animator) : base(interaction, animator)
    {
    }

    public override void Execute()
    {
        _animator.SetBool(_animIDSit, true);

        //CompleteCommand(); //?
    }

    public override void Exit()
    {
        CompleteCommand();
    }
}
