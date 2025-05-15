using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [field: SerializeField] public SpawnPointType Type { get; private set; }

    public bool IsEmpty { get; private set; }

    public void ChangeEmptyState(bool isEmpty)
    {
        IsEmpty = isEmpty;
    }
}