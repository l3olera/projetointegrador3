using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SmellTrail : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public bool canDrawPath = false;
    [SerializeField] private LineRenderer _path;
    [SerializeField] private float _pathHeightOffset = 1.25f;
    [SerializeField] private float _pathUpdateSpeed = 0.25f;

    private NavMeshTriangulation _triangulation;
    private Coroutine drawPathCoroutine;

    void Awake()
    {
        _triangulation = NavMesh.CalculateTriangulation();
    }

    void Start()
    {
        ReferenceManager.Instance.smellTrail = this; // Define a instância do SmellTrail
    }

    public void GenerateTrail()
    {
        if (drawPathCoroutine != null)
        {
            StopCoroutine(drawPathCoroutine);
        }

        drawPathCoroutine = StartCoroutine(DrawPath());
    }

    private IEnumerator DrawPath()
    {
        WaitForSeconds wait = new(_pathUpdateSpeed);
        NavMeshPath path = new();

        while (canDrawPath)
        {
            if (NavMesh.CalculatePath(player.position, target.position, NavMesh.AllAreas, path))
            {
                _path.positionCount = path.corners.Length;
                for (int i = 0; i < path.corners.Length; i++)
                {
                    Vector3 corner = path.corners[i];
                    corner.y += _pathHeightOffset;
                    _path.SetPosition(i, corner);
                }
            }
            else
            {
                Debug.LogError($"Unable to calculate a path on the NavMesh from {player.position} to {target.position}!");
            }
            yield return wait;
        }
    }

    public void ClearTrail()
    {

    }
}
