using System;
using UnityEngine;

[RequireComponent (typeof(UserInput))]
public class InteractionController : MonoBehaviour
{
    public event Action<InteractionData> OnInteract;

    [SerializeField] private CharacterMovementController _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterStateManager _stateManager;

    private void Start()
    {
        UserInput.OnInteractPressed += Interact;   
        
        if (!_player) _player = GetComponent<CharacterMovementController>();
        if (!_animator) _animator = GetComponent<Animator>();
        if (!_stateManager) _stateManager = GetComponent<CharacterStateManager>();
    }

    private void Interact(Vector2 currentPointerPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(currentPointerPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<InteractionTarget>(out InteractionTarget interactionTarget))
            {
                var interactionData = interactionTarget.Data;

                if (_stateManager.IsValidState(interactionData.AvailableFromStates))                   
                    OnInteract?.Invoke(interactionData);    
            }
        }
    }
}
