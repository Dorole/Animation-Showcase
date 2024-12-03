using System;
using System.Collections;
using UnityEngine;

public abstract class BaseCommand<EInteraction> where EInteraction : Enum
{
    public event Action OnCommandComplete;
    public EInteraction Interaction { get; private set; }
    public InteractionData InteractionData { get; private set; }
    protected Animator _animator;

    public virtual bool RequiresFinishPoint() => false;
    public virtual bool RequiresAutoMove() => false;

    public BaseCommand(EInteraction interaction, Animator animator)
    {
        Interaction = interaction;
        _animator = animator;
    }

    public void InitData(InteractionData data) => InteractionData = data;

    protected void CompleteCommand()
    {
        OnCommandComplete?.Invoke();
    }

    protected void ClearCommandListeners()
    {
        OnCommandComplete = null;
    }


    public abstract void Execute();

    /// <summary>
    /// Use when character doesn't change position/state
    /// </summary>
    public virtual void Clear() { }

    /// <summary>
    /// Use when character needs to change position/state
    /// </summary>
    public virtual void Exit()
    {
        Clear();
        //additional logic
    }
}
