using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PlayerSmellInput : MonoBehaviour
{
    private NavMeshAgent _agent;
    private SmellTrail _trail;
    private SmellTargetManager _manager;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_trail == null)
        {
            _trail = ReferenceManager.Instance.smellTrail;
        }

        if (_manager == null)
        {
            _manager = ReferenceManager.Instance.smellTargetManager;
        }

        //Se o jogador pressionar a tecla "C" ou "V", ativa o traço de cheiro
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V))
        {
            _trail.player = _agent; //Define o nav mesh agent do jogador

            _trail.target = _manager.GetCurrentTarget(); //Define o alvo do traço de cheiro
            _trail.canDrawPath = !_trail.canDrawPath; //Alterna o estado de desenho do traço
            _trail.GenerateTrail(); //Gera o traço de cheiro
        }
    }
}
