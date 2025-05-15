using UnityEngine;

[CreateAssetMenu(fileName = "PrefabsConfig", menuName = "Configs/Gameloop/PrefabsConfig")]
public class PrefabsConfig : ScriptableObject
{
    [field: SerializeField] public UIHandlerGameLoop UIHandlerGameLoopPrefab { get; private set; }
    [field: SerializeField] public Player PlayerPrefab { get; private set; }
    [field: SerializeField] public Level LevelPrefab { get; private set; }
}