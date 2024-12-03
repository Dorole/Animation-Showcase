using System;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public event Action OnExitAnimationComplete;

    public void OnAnimationComplete()
    {
        OnExitAnimationComplete?.Invoke();
    }
}
