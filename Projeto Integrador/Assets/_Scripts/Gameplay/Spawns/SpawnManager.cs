using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; } // Instância única do SpawnManager
    public List<SpawnPoint> spawnPoints;

    void Awake()
    {
        if (Instance != null && Instance != this) // Verifica se já existe uma instância
        {
            Destroy(this.gameObject); // Destroi o objeto atual se já houver uma instância
            return;
        }

        Instance = this; // Define a instância atual como a única instância
    }

    public void SpawnAt(int id, GameObject target)
    {
        var point = spawnPoints.Find(s => s.spawnID == id);
        point?.setter.Teleport(target);
    }
}
