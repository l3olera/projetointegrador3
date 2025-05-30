using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class InteractItem : MonoBehaviour
{
    [Header("Referências")]

    [SerializeField] private string _translateName; // Referência ao texto que vai traduzir na interação 
    [SerializeField] private InventoryController _ic;
    private SmellTargetManager smellManager; // Referência ao gerenciador de alvos de cheiro
    private TextInteractManager _textInteractManager; // Referência ao gerenciador de texto de interação

    [Header("Configurações de interação")]
    private GameObject _item; // Representa qual é o item do objeto

    public bool isInteractable; // Verifica se o objeto está interagível

    void Update()
    {
        if (smellManager == null)
        {
            smellManager = ReferenceManager.Instance.smellTargetManager; // Obtém a referência ao gerenciador de alvos de cheiro
        }

        if (_textInteractManager == null)
        {
            _textInteractManager = ReferenceManager.Instance.textInteractManager; // Obtém a referência ao gerenciador de texto de interação
        }

        if (isInteractable && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z))) // Verifica se o jogador está interagindo com o objeto
        {
            Interact(); // Chama a função de interação
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            _item = other.gameObject; // Obtém o tipo de item do objeto que colidiu
            isInteractable = true; // Define que o objeto é interagível quando o jogador entra na área de colisão
            _textInteractManager.canSetText = true; // Permite que o texto de interação seja definido
            string _itemName = _item.GetComponent<ItemType>().itemType.itemName; // Obtém o nome do item
            _textInteractManager.SetText(LocalizationManager.Instance.GetTranslation(_translateName), LocalizationManager.Instance.GetTranslation(_itemName)); // Define o texto de interação com a tradução
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            _item = null; // Limpa o tipo de item quando o jogador sai da área de colisão
            isInteractable = false; // Define que o objeto não é mais interagível quando o jogador sai da área de colisão
            _textInteractManager.canSetText = false; // Impede que o texto de interação seja definido
        }
    }

    void Interact()
    {
        if (_item != null) // Verifica se o tipo de item não é nulo
        {
            _ic.slot = _item.GetComponent<ItemType>().itemType; // Adiciona o item ao inventário
            _ic.SlotImage = _item.GetComponent<ItemType>().itemType.itemSprite; // Adiciona a imagem do item ao inventário
            Destroy(_item); // Destroi o objeto após a interação
            _item = null; // Limpa o tipo de item após a interação
            _textInteractManager.canSetText = false; // Limpa o texto de interação
            smellManager.NextTarget(); // Chama a função de próximo alvo do gerenciador de alvos de cheiro
        }
    }
}
