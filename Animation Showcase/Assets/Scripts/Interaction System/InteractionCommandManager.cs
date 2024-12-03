using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCommandManager : MonoBehaviour
{
    public static event Action<InteractionData> OnNewCommand;
    
    [SerializeField] private InteractionController _interactionController;
    [SerializeField] private CharacterMovementController _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationEventHandler _animationEvents;
    private Transform _interactionPoint;
    private InteractionCommandFactory _interactionCommandFactory;

    private BaseCommand<EInteractionType> _currentCommand;
    private BaseCommand<EInteractionType> _previousCommand;
    private Queue<InteractionData> _commandDataQueue = new Queue<InteractionData>();
    private bool _isCommandRunning = false; 
    
    private void Start()
    {
        _interactionCommandFactory = new InteractionCommandFactory();

        if (!_interactionController) _interactionController = GetComponent<InteractionController>();
        if (!_player) _player = GetComponent<CharacterMovementController>();
        if (!_animator) _animator = GetComponent<Animator>();
        if (!_animationEvents) _animationEvents = GetComponent<AnimationEventHandler>();

        _interactionController.OnInteract += HandleInteraction;
        _animationEvents.OnExitAnimationComplete += _animationEvents_OnExitAnimationComplete;

        CharacterStateManager.OnStatePersist += CharacterStateManager_OnStatePersist;
        CharacterStateManager.OnStateChanged += CharacterStateManager_OnStateChanged;
    }

    private void _animationEvents_OnExitAnimationComplete()
    {
        if (_previousCommand.RequiresFinishPoint())
        {
            MoveToFinishPoint(() =>
            {
                MoveToInteractionPoint();
            });
        }
        else
            MoveToInteractionPoint();
    }

    //see if these two can be merged
    private void CharacterStateManager_OnStatePersist()
    {
        _previousCommand.Clear();
        if (_currentCommand.RequiresAutoMove())
            MoveToInteractionPoint();
    }

    private void CharacterStateManager_OnStateChanged(ECharacterState newState)
    {
        //if (_isCommandRunning) return; //?
        
        if (_previousCommand == null)
        {
            MoveToInteractionPoint();
            return;
        }

        _previousCommand.Exit();

    }

    private void HandleInteraction(InteractionData interactionData)
    {
        _commandDataQueue.Enqueue(interactionData);

        if (!_isCommandRunning) //temp; what's the point?
        {
            InitiateNewCommand();
            MoveToInteractionPoint();
        }
    }

    private void InitiateNewCommand()
    {
        _previousCommand = _currentCommand;

        InteractionData interactionData = _commandDataQueue.Dequeue();
        _isCommandRunning = true;

        SetCurrentCommand(interactionData);
        OnNewCommand?.Invoke(_currentCommand.InteractionData);       
    }

    private void SetCurrentCommand(InteractionData interactionData)
    {
        _currentCommand = _interactionCommandFactory.CreateCommand(interactionData.InteractionType, _animator);
        _interactionPoint = interactionData.InteractionPoint;
        _currentCommand.InitData(interactionData);
    }

    private void MoveToInteractionPoint()
    {
        if (!_currentCommand.RequiresAutoMove()) //makes no sense this is here, should be outside
        {
            ExecuteCommand();
            return;
        }

        _player.AutoMove(_interactionPoint);

        void OnMoveComplete()
        {
            _player.OnAutoMoveComplete -= OnMoveComplete;
            ExecuteCommand();
        }

        _player.OnAutoMoveComplete += OnMoveComplete;
    }

    private void ExecuteCommand()
    {
        _currentCommand.Execute();

        //TEMPORARY!! - should be triggered after the animation is over
        _isCommandRunning = false;
    }

    private void MoveToFinishPoint(Action onFinishComplete)
    {
        _player.AutoMove(_previousCommand.InteractionData.FinishPoint);
        _player.OnAutoMoveComplete += OnMoveComplete;

        void OnMoveComplete()
        {
            _player.OnAutoMoveComplete -= OnMoveComplete;
            onFinishComplete?.Invoke();
        }

    }


}
