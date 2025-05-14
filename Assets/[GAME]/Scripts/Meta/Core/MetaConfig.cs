using Fusion;
using UnityEngine;

[CreateAssetMenu(fileName = "MetaConfig", menuName = "Configs/Meta/MetaConfig")]
public class MetaConfig : ScriptableObject
{
    [field: SerializeField] public UIHandlerMeta UIHandlerMetaPrefab { get; private set; }
    [field: SerializeField] public ConnectManager ConnectManagerPrefab { get; private set; }

    [field: Space(10)]
    [field: Header("Network settings")]
    [field: SerializeField] public NetworkRunner RunnerPrefab { get; private set; }
    [field: SerializeField] public int MaxPlayerCount { get; private set; } = 8;
}