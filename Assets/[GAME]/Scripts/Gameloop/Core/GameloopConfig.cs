using UnityEngine;

[CreateAssetMenu(fileName = "GameloopConfig", menuName = "Configs/Gameloop/GameloopConfig")]
public class GameloopConfig : ScriptableObject
{
    [field: SerializeField] public PrefabsConfig PrefabsConfig { get; private set; }
    [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }
}