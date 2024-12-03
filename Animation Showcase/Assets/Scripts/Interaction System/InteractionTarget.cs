using UnityEngine;

public class InteractionTarget : MonoBehaviour
{
    [SerializeField] private InteractionData _data;

    public InteractionData Data => _data; 

    public void SetDynamicInteractionPoint(Transform dynamicPoint)
    {
        _data.InteractionPoint = dynamicPoint;
    }
}


[System.Serializable]
public struct InteractionData
{
    public EInteractionType InteractionType;
    public ECharacterState AvailableFromStates;
    public ECharacterState ThisState;

    [Space(10)]
    public string AnimatorParam;

    [Space(10)]
    [Tooltip("The point on the object where the player has to walk up to etc.")]
    public Transform InteractionPoint;

    [Tooltip("The point on the object the character should align its root with.")]
    public Vector3 AlignPoint;

    [Tooltip("The point the character should move to once finished with interaction.")]
    public Transform FinishPoint; 
}



