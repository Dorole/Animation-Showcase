using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(InteractionController controller);
}

public enum InteractionType
{
    NONE,
    SIT,
    DRINK,
    TALK
}
