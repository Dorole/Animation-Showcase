using UnityEngine;

[RequireComponent (typeof(UserInput))]
public class InteractionController : MonoBehaviour
{
    //CLEAR THE COMMAND / TYPE?
    //Potentially should not be on the player character??

    [SerializeField] private PlayerInteractions _player;
    private InteractionType _currentInteraction = InteractionType.NONE;
    private IInteractCommand _currentInteractionCommand;


    private void Start()
    {
        UserInput.OnInteractPressed += InteractionResponse;       
        _player = GetComponent<PlayerInteractions>();
    }

    private void InteractionResponse(Vector2 currentPointerPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(currentPointerPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
                interactable.Interact(this);
        }
    }

    public void SetInteraction(InteractionType interactionType, Transform interactionPoint)
    {
        switch (interactionType)
        {
            case InteractionType.NONE:
                break;
            case InteractionType.SIT:
                _currentInteraction = InteractionType.SIT;
                _currentInteractionCommand = new SitCommand(_player, interactionPoint);
                break;
            case InteractionType.DRINK:
                break;
            case InteractionType.TALK:
                break;
            default:
                break;
        }

        _currentInteractionCommand.Execute();
    }
}
