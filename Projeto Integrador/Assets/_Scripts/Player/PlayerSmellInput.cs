using UnityEngine;
using UnityEngine.AI;

public class PlayerSmellInput : MonoBehaviour
{
    private NavMeshAgent _agent;
    private SmellTrail _trail;
    private SmellTargetManager _smellManager;
    private InputManager _im; // Referência ao gerenciador de entrada

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _smellManager = SmellTargetManager.Instance; // Obtém a referência ao SmellTargetManager
        _trail = SmellTrail.Instance; // Obtém a referência ao SmellTrai
        _im = InputManager.Instance; // Obtém a instância do InputManager
    }

    void Update()
    {
        //Se o jogador pressionar a tecla de farejar ativa o traço de cheiro
        if (_im.IsSmellKeyPressed())
        {
            _trail.player = _agent; //Define o nav mesh agent do jogador

            _trail.target = _smellManager.GetCurrentTarget(); //Define o alvo do traço de cheiro
            _trail.canDrawPath = !_trail.canDrawPath; //Alterna o estado de desenho do traço
            _trail.GenerateTrail(); //Gera o traço de cheiro
        }
    }
}
