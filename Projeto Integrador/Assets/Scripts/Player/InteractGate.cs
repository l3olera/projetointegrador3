using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class InteractGate : MonoBehaviour
{
    [SerializeField] private GateController _gateController; // Referência ao GateController
    [SerializeField] private InventoryController _inventoryController; // Referência ao InventoryController
    [SerializeField] private TextMeshProUGUI _interactText; // Referência ao texto de interação
    [SerializeField] private LocalizedString _textTranslateInteract; // Referência ao texto que vai traduzir na interação 
    [SerializeField] private ObjectivesController _objectivesController; // Referência ao ObjectivesController
    private string _cachedTranslation; // Variável para armazenar a tradução em cache

    void Start()
    {
        _textTranslateInteract.GetLocalizedStringAsync().Completed += handle => // Obtém o texto traduzido
        {   
            _cachedTranslation = handle.Result; // Armazena a tradução em cache
        };   
        
    }

    void Update()
    {
        if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z)) && _gateController.canOpen){ // Verifica se o jogador está interagindo com o objeto{
            _gateController.OpenGate(); // Chama a função OpenGate do GateController
            _inventoryController.slot = null; // Limpa o slot do inventário
            _inventoryController.SlotImage = null; // Limpa a imagem do slot do inventário
            _interactText.text = ""; // Limpa o texto de interação
            _gateController.canOpen = false; // Define que o portão não pode mais ser aberto
        }    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate")) // Verifica se o objeto que colidiu tem a tag "Player"
        {
            if(_inventoryController.slot != null && _inventoryController.slot.itemID == 1){ // Verifica se o slot do inventário não é nulo
                _gateController.canOpen = true; // Define que o portão pode ser aberto
                _interactText.text = _cachedTranslation; // Atualiza o texto de interação
            }
        }

        if(other.CompareTag("TriggerClose"))
        {
            _gateController.CloseGate(); // Chama a função CloseGate do GateController
            _gateController.canOpen = false; // Define que o portão não pode mais ser aberto
            
            if(_objectivesController.CurrentObjective == 1){
                _objectivesController.IncreaseActIndex(); // Chama a função para aumentar o índice do objetivo atual. Trocando, assim, o ato.
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gate")) // Verifica se o objeto que colidiu tem a tag "Player"
        {
            _interactText.text = ""; // Limpa o texto de interação quando o jogador sai da área de colisão
        }
    }
}
