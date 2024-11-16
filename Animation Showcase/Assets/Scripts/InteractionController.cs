using UnityEngine;

[RequireComponent (typeof(UserInput))]
public class InteractionController : MonoBehaviour
{
    [SerializeField] private CharacterMovementController _player;
    [SerializeField] private Animator _animator;
    
    private IInteractCommand _currentInteractionCommand;


    private void Start()
    {
        UserInput.OnInteractPressed += Interact;       
        if (!_player) _player = GetComponent<CharacterMovementController>();
        if (!_animator) _animator = GetComponent<Animator>();
    }

    private void Interact(Vector2 currentPointerPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(currentPointerPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<InteractionTarget>(out InteractionTarget interactionTarget))
            {
                var interactionData = interactionTarget.GetData();
                ExecuteInteractionCommand(interactionData.InteractionType, interactionData.InteractionPoint);                              
            }
        }
    }

    public void ExecuteInteractionCommand(InteractionType currentInteraction, Transform interactionPoint)
    {
        switch (currentInteraction)
        {
            case InteractionType.NONE:
                break;
            case InteractionType.SIT:
                _currentInteractionCommand = new SitCommand(_player, _animator, interactionPoint);
                break;
            case InteractionType.DRINK:
                break;
            case InteractionType.TALK:
                break;
            default:
                break;
        }

        _currentInteractionCommand.MoveToInteractionPoint();
    }
}
