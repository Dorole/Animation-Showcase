using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;

//nepotrebno?
public class PlayerInteractions : MonoBehaviour
{
    private Queue<IInteractCommand> _commandQueue = new Queue<IInteractCommand>();
    private CharacterMovementController _movementController;


    private void Start()
    {
        _movementController = GetComponent<CharacterMovementController>();
        //_movementController.OnAutoMoveComplete += ExecuteNextCommand;
    }

    private void OnDestroy()
    {
        //_movementController.OnAutoMoveComplete -= ExecuteNextCommand;
    }

    public void AddCommand(IInteractCommand command)
    {
        _commandQueue.Enqueue(command);
        if (_commandQueue.Count == 1) 
            ExecuteNextCommand();
    }

    private void ExecuteNextCommand()
    {
        if (_commandQueue.Count > 0 )
        {
            IInteractCommand command = _commandQueue.Dequeue();
            command.MoveToInteractionPoint();
        }
    }



}
