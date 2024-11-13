using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitCommand : IInteractCommand
{
    private PlayerInteractions _player;
    private Transform _interactionPoint; 

    public SitCommand(PlayerInteractions player, Transform interactionPoint)
    {
        _player = player;
        _interactionPoint = interactionPoint;
    }

    public void Execute()
    {
        _player.Sit(_interactionPoint);
    }
}
