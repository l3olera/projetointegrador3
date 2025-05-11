using UnityEngine;
using System.Collections.Generic;

public class SmellTrail : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public GameObject trailPrefab;
    public float spacing = 1f;
    public int maxSteps = 20;

    private List<GameObject> trailSteps = new();

    public void GenerateTrail()
    {
        ClearTrail();

        Vector3 direction = (target.position - player.position).normalized;
        float distance = Vector3.Distance(player.position, target.position);
        int steps = Mathf.Min(maxSteps, Mathf.FloorToInt(distance / spacing));

        for (int i = 1; i <= steps; i++)
        {
            Vector3 pos = player.position + direction * spacing * i;
            GameObject trail = Instantiate(trailPrefab, pos, Quaternion.identity);
            trailSteps.Add(trail);
        }
    }

    public void ClearTrail()
    {
        foreach (var step in trailSteps)
        {
            Destroy(step);
        }
        trailSteps.Clear();
    }
}
