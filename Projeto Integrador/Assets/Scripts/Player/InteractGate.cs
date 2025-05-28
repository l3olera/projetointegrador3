using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class InteractGate : MonoBehaviour
{
    [SerializeField] private GateController _gateController; // Referência ao GateController
    [SerializeField] private InventoryController _inventoryController; // Referência ao InventoryController
    [SerializeField] private TextMeshProUGUI _interactText; // Referência ao texto de interação
    [SerializeField] private ObjectivesController _objectivesController; // Referência ao ObjectivesController
    [SerializeField] private string _translateName; // Referência ao texto que vai traduzir na interação 
    private SmellTargetManager _smellManager; // Referência ao gerenciador de alvos de cheiro
    private TextInteractManager _textInteractManager; // Referência ao gerenciador de texto de interação


    void Update()
    {
        if (_smellManager == null)
        {
            _smellManager = ReferenceManager.Instance.smellTargetManager; // Obtém a referência ao gerenciador de alvos de cheiro
        }

        if (_textInteractManager == null)
        {
            _textInteractManager = ReferenceManager.Instance.textInteractManager; // Obtém a referência ao gerenciador de texto de interação
        }

        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z)) && _gateController.canOpen)
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

        if (other.CompareTag("TriggerClose"))
        {
            _gateController.CloseGate(); // Chama a função CloseGate do GateController
            _gateController.canOpen = false; // Define que o portão não pode mais ser aberto

            if (_objectivesController.CurrentObjective == 1)
            {
                _objectivesController.IncreaseActIndex(); // Chama a função para aumentar o índice do objetivo atual. Trocando, assim, o ato.
                _smellManager.NextTarget(); // Chama a função para ir para o próximo alvo
            }
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
