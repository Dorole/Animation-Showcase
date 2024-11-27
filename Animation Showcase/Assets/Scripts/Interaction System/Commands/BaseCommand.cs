using System;
using UnityEngine;

public abstract class BaseCommand<EInteraction> where EInteraction : Enum
{
    public event Action OnCommandComplete; 
    public EInteraction Interaction { get; private set; }
    protected Animator _animator;

    public BaseCommand(EInteraction interaction, Animator animator)
    {
        Interaction = interaction;
        _animator = animator;
    }

    protected void CompleteCommand()
    {
        OnCommandComplete?.Invoke();
    }

    public void Init(Animator animator) => _animator = animator; //to ctr??

    public abstract void Execute();
    public abstract void Exit();
}
