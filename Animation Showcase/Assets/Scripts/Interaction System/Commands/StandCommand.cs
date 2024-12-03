using UnityEngine;

public class StandCommand : BaseCommand<EInteractionType> 
{
    private int _animIDStand = Animator.StringToHash("Stand"); //izvana?

    public StandCommand(EInteractionType interaction, Animator animator) : base(interaction, animator)
    {
        //set dynamic interaction point?
        Debug.Log("STAND COMMAND CREATED"); 
    }

    public override void Execute()
    {
        _animator.SetTrigger(_animIDStand);
    }
}
