using UnityEngine;
using UnityEngine.AI;

public class PlayerSmellInput : MonoBehaviour
{
    private NavMeshAgent _agent;
    private SmellTrail _trail;
    private SmellTargetManager _smellManager;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _smellManager = SmellTargetManager.Instance; // Obtém a referência ao SmellTargetManager
        _trail = SmellTrail.Instance; // Obtém a referência ao SmellTrail
    }

    void Update()
    {
        //Se o jogador pressionar a tecla "C" ou "V", ativa o traço de cheiro
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V))
        {
            _trail.player = _agent; //Define o nav mesh agent do jogador

            _trail.target = _smellManager.GetCurrentTarget(); //Define o alvo do traço de cheiro
            _trail.canDrawPath = !_trail.canDrawPath; //Alterna o estado de desenho do traço
            _trail.GenerateTrail(); //Gera o traço de cheiro
        }
    }
}
