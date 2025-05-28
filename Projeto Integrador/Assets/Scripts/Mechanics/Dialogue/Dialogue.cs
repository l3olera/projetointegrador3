using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class Dialogue : MonoBehaviour
{
    public DialogueSequence[] dialogueSequence; // Array de sequências de diálogos

    public int requiredIdItem = -1; // ID do item necessário para continuar um próximo dialogo o diálogo

    [SerializeField] private bool _onRadious; // Indica se o jogador está dentro do raio de interação
    [SerializeField] private string _translateName; // Nome da tradução para o texto de interação
    private bool[] _dialogueOccured; // Array para verificar se o diálogo já ocorreu
    private DialogueControl _dc; // Referência ao script que controla os diálogos
    private InventoryController _ic; // Referência ao script que controla o inventário
    private ObjectivesController _oc; // Referência ao script que controla os objetivos
    private SmellTargetManager _smellManager; // Referência ao gerenciador de alvos de cheiro
    private TextInteractManager _dialogueTextInteract; // Referência ao gerenciador de texto de interação

    void Start()
    {
        _dialogueOccured = new bool[dialogueSequence.Length]; // Inicializa o array com o tamanho das sequências de diálogo
        for (int i = 0; i < _dialogueOccured.Length; i++)
        {
            _dialogueOccured[i] = false;
        }
    }

    void Update()
    {
        if (_dc == null) // Verifica se o DialogueControl não foi encontrado
        {
            _dc = ReferenceManager.Instance.dialogueControl; // Tenta encontrar novamente o DialogueControl
        }

        if (_ic == null) // Verifica se o InventoryController não foi encontrado
        {
            _ic = ReferenceManager.Instance.inventoryController; // Tenta encontrar novamente o InventoryController
        }

        if (_oc == null) // Verifica se o InventoryController não foi encontrado
        {
            _oc = ReferenceManager.Instance.objectivesController; // Tenta encontrar novamente o InventoryController
        }

        if (_smellManager == null) // Verifica se o InventoryController não foi encontrado
        {
            _smellManager = ReferenceManager.Instance.smellTargetManager; // Tenta encontrar novamente o InventoryController
        }

        if (_dialogueTextInteract == null) // Verifica se o TextInteractManager não foi encontrado
        {
            _dialogueTextInteract = ReferenceManager.Instance.textInteractManager; // Tenta encontrar novamente o TextInteractManager
        }

        if (_onRadious && _dc.canInteract)
        {
            _dialogueTextInteract.SetText(LocalizationManager.Instance.GetTranslation(_translateName)); // Obtém a tradução do texto de interação); // Define o texto de interação

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z))
            {
                if (!_ic.HasItemById(requiredIdItem))
                {
                    _dc.Speech(dialogueSequence[0].lines); // Passa o array de falas para o DialogueControl
                    if (!_dialogueOccured[0]) // Verifica se o diálogo já ocorreu
                    {
                        _dialogueOccured[0] = true; // Se não ocorreu, então marca o diálogo como ocorrido
                        _smellManager.NextTarget(); // Chama a função para ir para o próximo alvo
                    }
                }
                else
                {
                    _dc.Speech(dialogueSequence[1].lines); // Passa o array de falas para o DialogueControl

                    if (!_dialogueOccured[1]) // Verifica se o diálogo já ocorreu
                    {
                        _dialogueOccured[1] = true; // Marca o diálogo como ocorrido
                        _smellManager.NextTarget(); // Chama a função para ir para o próximo alvo
                    }

                    _ic.RemoveItem(); // Remove o item do inventário
                    _oc.IncreaseActIndex(); // Aumenta o índice do ato atual

                }

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se o objeto detectado é o jogador
        {
            _onRadious = true; // Marca que o jogador está dentro do raio

            _dialogueTextInteract.canSetText = true; // Permite que o jogador mude o texto de interação
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se o objeto que saiu é o jogador
        {
            _onRadious = false; // Marca que o jogador saiu do raio

            _dialogueTextInteract.canSetText = false; // Impede que o jogador mude o texto de interação
        }
    }
}
