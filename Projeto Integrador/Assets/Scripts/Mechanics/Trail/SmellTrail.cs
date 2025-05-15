using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SmellTrail : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public bool canDrawPath = false;
    [SerializeField] private LineRenderer _path;
    [SerializeField] private float _pathHeightOffset;
    [SerializeField] private float _pathUpdateSpeed = 0.25f;

    private Coroutine drawPathCoroutine;

    void Start()
    {
        ReferenceManager.Instance.smellTrail = this; // Define a instância do SmellTrail
        _path.gameObject.SetActive(false); // Desativa o LineRenderer inicialmente
    }

    public void GenerateTrail()
    {
        if (drawPathCoroutine != null)
        {
            StopCoroutine(drawPathCoroutine);
        }

        if (canDrawPath)
        {
            _path.gameObject.SetActive(true);
            drawPathCoroutine = StartCoroutine(DrawPath());
        }
        else
        {
            _path.gameObject.SetActive(false);
        }
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
                    _path.SetPosition(i, path.corners[i] + Vector3.up * _pathHeightOffset);
                }
            }
            else
            {
                Debug.LogError($"Unable to calculate a path on the NavMesh from {player.position} to {target.position}!");
            }
            yield return wait;
        }
    }
}
