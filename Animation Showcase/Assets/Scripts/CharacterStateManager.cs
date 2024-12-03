using System;
using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    public static event Action OnStatePersist;
    public static event Action<ECharacterState> OnStateChanged;

    [SerializeField] private ECharacterState _startingState = ECharacterState.STANDING;
    private ECharacterState _currentState;
    public ECharacterState CurrentState { get { return _currentState; } }

    private void Awake()
    {
        _currentState = _startingState;
    }

    private void Start()
    {
        InteractionCommandManager.OnNewCommand += SetState;
    }

    public void SetState(InteractionData data)
    {
        ECharacterState newState = data.ThisState;

        if (newState == _currentState)
        {
            OnStatePersist?.Invoke();
            return;
        }

        _currentState = newState;
        OnStateChanged?.Invoke(_currentState);
    }

    public bool IsValidState(ECharacterState newState)
    {
        return newState.HasFlag(_currentState);
    }
}
