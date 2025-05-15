using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public float StartHealth { get; private set; }
    [field: SerializeField] public float StartArmor { get; private set; }
    [field: SerializeField] public float StartSpeed { get; private set; }
}