using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class InteractItem : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private TextMeshProUGUI _interactText; // Referência ao ícone de interação
    [SerializeField] private LocalizedString _textTranslateInteract; // Referência ao texto que vai traduzir na interação 
    [SerializeField] private InventoryController _ic;

    [Header("Configurações de interação")]
    private GameObject _item; // Representa qual é o item do objeto
    private bool _isDestroyed = false; // Verifica se o objeto foi destruído
    private SmellTargetManager smellManager; // Referência ao gerenciador de alvos de cheiro
    public bool isInteractable; // Verifica se o objeto está interagível
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            _item = other.gameObject; // Obtém o tipo de item do objeto que colidiu
            isInteractable = true; // Define que o objeto é interagível quando o jogador entra na área de colisão

            // Obtém o nome traduzido do item
            var localizedString = _item.GetComponent<ItemType>().itemType.itemName;
            localizedString.GetLocalizedStringAsync().Completed += handle =>
            {
                _textTranslateInteract.GetLocalizedStringAsync().Completed += promptHandle =>
                {
                    _interactText.text = $"{promptHandle.Result} {handle.Result}";
                };
            };
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            _item = null; // Limpa o tipo de item quando o jogador sai da área de colisão
            isInteractable = false; // Define que o objeto não é mais interagível quando o jogador sai da área de colisão
            _interactText.text = ""; // Deixa vazio o texto de interação quando o jogador sai da área de colisão
        }
    }

    void Update()
    {
        if (smellManager == null)
        {
            smellManager = ReferenceManager.Instance.smellTargetManager; // Obtém a referência ao gerenciador de alvos de cheiro
        }

        if (_isDestroyed && !string.IsNullOrEmpty(_interactText.text))
        { //Verifica se o objeto foi destruído e se há texto no interactText. Assim, não deixa o texto de interação ficar aparecendo. Já que pode dar um bug de ficar aparecendo o texto de interação mesmo depois do objeto ter sido destruído por conta da tabela de tradução fazer uma chamada assíncrona
            _isDestroyed = false; // Reseta a variável para impedir dessa estrutura ficar acontecendo
            _interactText.text = "";
        }

        if (isInteractable && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z))) // Verifica se o jogador está interagindo com o objeto
        {
            Interact(); // Chama a função de interação
        }
    }

    void Interact()
    {
        if (_item != null) // Verifica se o tipo de item não é nulo
        {
            _ic.slot = _item.GetComponent<ItemType>().itemType; // Adiciona o item ao inventário
            _ic.SlotImage = _item.GetComponent<ItemType>().itemType.itemSprite; // Adiciona a imagem do item ao inventário
            Destroy(_item); // Destroi o objeto após a interação
            _isDestroyed = true; // Define que o objeto foi destruído
            _item = null; // Limpa o tipo de item após a interação
            _interactText.text = "";
            smellManager.NextTarget(); // Chama a função de próximo alvo do gerenciador de alvos de cheiro
        }
    }
}
