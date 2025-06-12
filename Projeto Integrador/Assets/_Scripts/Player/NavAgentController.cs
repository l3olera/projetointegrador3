using UnityEngine;
using UnityEngine.AI;

public class NavAgentController : MonoBehaviour
{
    private NavMeshAgent _navAgent;

    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    public void DisableAgent()
    {
        if (_navAgent != null)
        {
            _navAgent.enabled = false;
        }
    }

    public void EnableAgent()
    {
        if (_navAgent != null)
        {
            _navAgent.enabled = true;
        }
    }
}
