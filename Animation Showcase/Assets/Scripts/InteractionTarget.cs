using UnityEngine;

public class InteractionTarget : MonoBehaviour
{
    [SerializeField] private InteractionData _data;

    public InteractionData GetData() => _data;
}

[System.Serializable]
public struct InteractionData
{
    public InteractionType InteractionType;

    [Tooltip("The point on the object where the player has to walk up to etc.")]
    public Transform InteractionPoint;

    [Tooltip("The point on the object the character should align its root with.")]
    public Vector3 AlignPoint;

    [Tooltip("The point the character should move to once finished with interaction.")]
    public Vector3 FinishPoint;
}



