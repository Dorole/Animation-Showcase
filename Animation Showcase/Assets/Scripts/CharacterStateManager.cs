using UnityEngine;

public class CharacterStateManager : MonoBehaviour
{
    [SerializeField] private ECharacterState _startingState = ECharacterState.STANDING;
    private ECharacterState _currentState;
    public ECharacterState CurrentState { get { return _currentState; } }

    private void Awake()
    {
        _currentState = _startingState;
    }

    public void SetState(ECharacterState newState)
    {
        if (newState == _currentState) return;
        _currentState = newState;
    }

    public bool IsValidState(ECharacterState newState)
    {
        return newState.HasFlag(_currentState);
    }
}
