using TMPro;
using UnityEngine;

public class InteractGate : MonoBehaviour
{
    [SerializeField] private GateController _gateController; // Referência ao GateController
    [SerializeField] private InventoryController _inventoryController; // Referência ao InventoryController
    [SerializeField] private TextMeshProUGUI _interactText; // Referência ao texto de interação
    [SerializeField] private ObjectivesController _objectivesController; // Referência ao ObjectivesController
    [SerializeField] private AudioSource _windSound; // Referência ao áudio de vento
    [SerializeField] private string _translateName; // Referência ao texto que vai traduzir na interação 
    private SmellTargetManager _smellManager; // Referência ao gerenciador de alvos de cheiro
    private TextInteractManager _textInteractManager; // Referência ao gerenciador de texto de interação
    private InputManager _im; // Referência ao gerenciador de entrada

    void Start()
    {
        _smellManager = SmellTargetManager.Instance; // Obtém a referência ao SmellTargetManager
        _textInteractManager = TextInteractManager.Instance; // Obtém a referência ao TextInteractManager
        _im = InputManager.Instance; // Obtém a instância do InputManager
    }

    void Update()
    {
        if (_im.IsInteractKeyPressed() && _gateController.canOpen)
        { // Verifica se o jogador está interagindo com o objeto
            _gateController.OpenGate(); // Chama a função OpenGate do GateController
            _inventoryController.RemoveItem(); // Chama a função para remover o item do inventário
            _textInteractManager.canSetText = false; // Limpa o texto de interação
            _gateController.canOpen = false; // Define que o portão não pode mais ser aberto
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate")) // Verifica se o objeto que colidiu tem a tag "Player"
        {
            if (_inventoryController.slot != null && _inventoryController.slot.itemID == 1)
            { // Verifica se o slot do inventário não é nulo
                _gateController.canOpen = true; // Define que o portão pode ser aberto
                _textInteractManager.canSetText = true; // Permite que o texto de interação seja definido
                _textInteractManager.SetText(LocalizationManager.Instance.GetTranslation(_translateName)); // Define o texto de interação com a tradução em cache
            }
        }

        if (other.CompareTag("TriggerClose") && _objectivesController.CompareAct(1))
        {
            _gateController.CloseGate(); // Chama a função CloseGate do GateController
            _gateController.canOpen = false; // Define que o portão não pode mais ser aberto

            if (_windSound != null)
                _windSound.Play(); // Toca o som de vento se a referência não for nula

            _objectivesController.IncreaseActIndex(); // Chama a função para aumentar o índice do objetivo atual. Trocando, assim, o ato.
            _smellManager.NextTarget(); // Chama a função para ir para o próximo alvo
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gate")) // Verifica se o objeto que colidiu tem a tag "Player"
        {
            _textInteractManager.canSetText = false; // Impede que o texto de interação seja definido
        }
    }
}
