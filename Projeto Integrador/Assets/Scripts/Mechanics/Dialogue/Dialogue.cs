using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class Dialogue : MonoBehaviour
{
    public DialogueSequence[] dialogueSequence; // Array de sequências de diálogos

    public int requiredIdItem = -1; // ID do item necessário para continuar um próximo dialogo o diálogo
    public LayerMask playerLayer; // Define a camada do jogador para detectar proximidade
    public float radious; // Raio da detecção de proximidade do NPC

    [SerializeField] private bool _onRadious; // Indica se o jogador está dentro do raio de interação
    [SerializeField] private LocalizedString _textTranslateInteract; // Referência ao texto que vai traduzir na interação
    private bool[] _dialogueOccured; // Array para verificar se o diálogo já ocorreu
    [SerializeField] private TextMeshProUGUI _interactText; // Referência ao texto de interação
    private DialogueControl _dc; // Referência ao script que controla os diálogos
    private InventoryController _ic; // Referência ao script que controla o inventário
    private ObjectivesController _oc; // Referência ao script que controla os objetivos
    private SmellTargetManager _smellManager; // Referência ao gerenciador de alvos de cheiro

    void Start()
    {
        _dialogueOccured = new bool[dialogueSequence.Length]; // Inicializa o array com o tamanho das sequências de diálogo
        for (int i = 0; i < _dialogueOccured.Length; i++)
        {
            _dialogueOccured[i] = false;
        }
    }

    void FixedUpdate()
    {
        Interact(); // Chama a verificação de interação com o NPC a cada atualização da física do jogo
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

        if (_onRadious && _dc.canInteract)
        {
            _textTranslateInteract.GetLocalizedStringAsync().Completed += handle =>
            {
                _interactText.text = handle.Result;
            };

            // Se o jogador pressionar "E" ou "Z", estiver no raio de interação e puder interagir
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
        else
        {
            _interactText.text = ""; // Limpa o texto de interação se o jogador não estiver no raio ou não puder interagir
        }
    }

    // Método que verifica se o jogador está dentro do raio de interação
    public void Interact()
    {
        _onRadious = false; // Assume que o jogador não está perto

        Collider[] hits = Physics.OverlapSphere(transform.position, radious, playerLayer); // Cria uma esfera invisível que detecta colisões dentro do raio definido

        foreach (Collider hit in hits) // Percorre todos os objetos detectados na esfera
        {

            if (hit.CompareTag("Player")) // Verifica se o objeto detectado é o jogador
            {
                _onRadious = true; // Marca que o jogador está dentro do raio
                break; // Sai do loop assim que encontrar o jogador, evitando verificações desnecessárias
            }
        }
    }

    // Método que desenha uma esfera no editor para visualizar a área de detecção do NPC    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radious); // Desenha a esfera com o raio definido no editor
    }
}
