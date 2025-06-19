using UnityEngine;

public class SmellTargetManager : MonoBehaviour
{
    public Transform[] act1Targets; // alvos para o ato 1
    public Transform[] act2Targets; // alvos para o ato 2
    public Transform[] act3Targets; // alvos para o ato 3
    public Transform[] act4Targets; // alvos para o ato 4

    [SerializeField] private int _currentTargetIndex = 0;
    private Transform[] _currentActTargets;
    private ObjectivesController _oc;
    private SmellTrail _smellTrail;

    public static SmellTargetManager Instance { get; private set; } // Instância única do SmellTargetManager

    void Awake()
    {
        if (Instance != null && Instance != this) // Verifica se já existe uma instância
        {
            Destroy(this.gameObject); // Destroi o objeto atual se já houver uma instância
            return; // Sai do método para evitar duplicação
        }
        Instance = this; // Inicializa a instância única do SmellTargetManager   
    }

    void Start()
    {
        _oc = ObjectivesController.Instance; // Obtém a referência ao ObjectivesController
        _smellTrail = SmellTrail.Instance; // Obtém a referência ao SmellTrail

        // Começa com Ato 1
        _currentActTargets = act1Targets;
    }

    public Transform GetCurrentTarget() => _currentActTargets[_currentTargetIndex];

    public void NextTarget()
    {
        _currentTargetIndex++;

        if (_currentTargetIndex >= _currentActTargets.Length)
        {
            SwitchAct();
        }
        _smellTrail.target = GetCurrentTarget(); // Atualiza o alvo do traço de cheiro
        _smellTrail.GenerateTrail(); // Gera o traço de cheiro
    }

    public void SwitchAct()
    {
        _currentTargetIndex = 0;
        if (_oc)
        {
            switch (_oc.CurrentObjective)
            {
                case 1:
                    _currentActTargets = act1Targets;
                    break;
                case 2:
                    _currentActTargets = act2Targets;
                    break;
                case 3:
                    _currentActTargets = act3Targets;
                    break;
                case 4:
                    _currentActTargets = act4Targets;
                    break;
            }
        }
    }
}

