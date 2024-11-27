using UnityEngine;

public class InteractionCommandFactory 
{
    public BaseCommand<EInteractionType> CreateCommand(EInteractionType interaction, Animator animator)
    {
        switch (interaction)
        {
            case EInteractionType.STAND:
                break;
            case EInteractionType.SIT:
                return new SitCommand(EInteractionType.SIT, animator);
            case EInteractionType.STAND_UP:
                break;
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
