using UnityEngine;

public class TriggerEndPersecution : MonoBehaviour
{
    [SerializeField] private PersecutionController _persecutionController; // Referência ao controlador de perseguição 
    [SerializeField] private GameObject _invisibleWalls; // Referência ao objeto que contém as paredes invisíveis para impedir que o jogador volte para trás
<<<<<<< HEAD
=======
    [SerializeField] private GateController _gateController;
    [SerializeField] private GameObject _gateTrigger;
    private SmellTargetManager _smell;

    void Start()
    {
        _smell = SmellTargetManager.Instance;
    }
>>>>>>> Development
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_persecutionController != null)
            {
                _persecutionController.managedEscape = true; // Marca que o jogador conseguiu escapar
                _invisibleWalls.SetActive(true); // Ativa as paredes invisíveis para impedir que o jogador volte atrás
<<<<<<< HEAD
=======
                _gateTrigger.SetActive(false);
                _gateController.OpenGate();
                _smell.NextTarget();
>>>>>>> Development
            }
        }
    }
}
