using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public void SpawnPlayer(int id)
    {
        SpawnManager.Instance.SpawnAt(id, gameObject);
    }
}
