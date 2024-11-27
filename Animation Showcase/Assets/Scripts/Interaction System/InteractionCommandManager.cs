using UnityEngine;

public class InteractionCommandManager : MonoBehaviour
{
    [SerializeField] private InteractionController _interactionController;
    [SerializeField] private CharacterMovementController _player;
    [SerializeField] private Animator _animator;
    private Transform _interactionPoint;
    private InteractionCommandFactory _interactionCommandFactory;

    private BaseCommand<EInteractionType> _currentCommand;

    private void Start()
    {
        _interactionCommandFactory = new InteractionCommandFactory();

        if (!_interactionController) _interactionController = GetComponent<InteractionController>();
        if (!_player) _player = GetComponent<CharacterMovementController>();
        if (!_animator) _animator = GetComponent<Animator>();

        _interactionController.OnInteract += HandleInteractionCommand;
    }

    private void HandleInteractionCommand(InteractionData interactionData)
    {
        SetCurrentCommand(interactionData.InteractionType);

        _interactionPoint = interactionData.InteractionPoint;
        MoveToInteractionPoint();
    }

    private void SetCurrentCommand(EInteractionType interactionType)
    {
        _currentCommand = _interactionCommandFactory.CreateCommand(interactionType, _animator);
        _currentCommand.OnCommandComplete += DisposePreviousCommand; //hmmmm????
    }

    private void MoveToInteractionPoint() 
    {
        _player.AutoMove(_interactionPoint);
        _player.OnAutoMoveComplete += _currentCommand.Execute;
    }

    private void DisposePreviousCommand()
    {
        _player.OnAutoMoveComplete -= _currentCommand.Execute;
        _currentCommand.OnCommandComplete -= DisposePreviousCommand;
        _currentCommand = null;
    }
}
