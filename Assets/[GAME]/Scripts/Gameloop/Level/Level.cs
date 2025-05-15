using UnityEngine;

public class Level : MonoBehaviour
{
    [field: SerializeField] public SpawnPoint[] spawnPoints { get; private set; }
}