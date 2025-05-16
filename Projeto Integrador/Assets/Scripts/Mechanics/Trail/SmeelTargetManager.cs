using UnityEngine;

public class SmellTargetManager : MonoBehaviour
{
    public Transform[] act1Targets; // alvos para o ato 1
    public Transform[] act2Targets; // alvos para o ato 2
    public Transform[] act3Targets; // alvos para o ato 3
    public Transform[] act4Targets; // alvos para o ato 4

    private int _currentTargetIndex = 0;
    private Transform[] _currentActTargets;
    private ObjectivesController _objectivesController;

    void Start()
    {
        ReferenceManager.Instance.smellTargetManager = this; // Define a instância do SmellTargetManager

        // Começa com Ato 1
        _currentActTargets = act1Targets;
    }

    void Update()
    {
        if (_objectivesController == null)
        {
            _objectivesController = ReferenceManager.Instance.objectivesController;
        }
    }

    public Transform GetCurrentTarget() => _currentActTargets[_currentTargetIndex];

    public void NextTarget()
    {
        _currentTargetIndex++;
        if (_currentTargetIndex >= _currentActTargets.Length)
            SwitchAct();
    }

    public void SwitchAct()
    {
        _currentTargetIndex = 0;
        if (_objectivesController)
        {
            switch (_objectivesController.CurrentObjective)
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

