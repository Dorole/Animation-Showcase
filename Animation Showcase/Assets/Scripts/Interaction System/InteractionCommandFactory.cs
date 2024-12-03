using UnityEngine;

public class InteractionCommandFactory 
{
    public BaseCommand<EInteractionType> CreateCommand(EInteractionType interaction, Animator animator)
    {
        switch (interaction)
        {
            case EInteractionType.SIT:
                return new SitCommand(EInteractionType.SIT, animator);
            case EInteractionType.STAND_UP:
                return new StandCommand(EInteractionType.STAND_UP, animator);
            case EInteractionType.DRINK:
                break;
            case EInteractionType.TALK:
                break;
            default:
                break;
        }

        return null; //makni
    }
}
