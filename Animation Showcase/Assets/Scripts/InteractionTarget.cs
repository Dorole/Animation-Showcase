using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTarget : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractionType _interactionType;

    [Tooltip("The point on the object where the player has to walk up to etc.")]
    [SerializeField] private Transform _interactionPoint;

    //Ok for single interactions
    //For multiple - implement like a context menu or some other type of selection mechanism
    public void Interact(InteractionController controller)
    {
        switch (_interactionType)
        {
            case InteractionType.SIT:
                controller.SetInteraction(InteractionType.SIT, _interactionPoint);
                break;
            case InteractionType.DRINK:
                controller.SetInteraction(InteractionType.DRINK, _interactionPoint);
                break;
            case InteractionType.TALK:
                controller.SetInteraction(InteractionType.TALK, _interactionPoint);
                break;
            default:
                break;
        }
    }
}
