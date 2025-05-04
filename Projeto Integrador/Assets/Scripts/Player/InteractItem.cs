using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractItem : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private TextMeshProUGUI _interactText; // Referência ao ícone de interação
    [SerializeField] private string _textTranslateInteract; // Referência ao texto que vai traduzir na interação 
    [SerializeField] private InventoryController _ic;

    [Header("Configurações de interação")]
    private GameObject _item; // Representa qual é o item do objeto
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
                _interactText.text = $"Pressione E ou Z para coletar {handle.Result}";
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
        if (isInteractable && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z))) // Verifica se o jogador está interagindo com o objeto
        {
            Interact(); // Chama a função de interação
        }   
    }

    void Interact(){
        if (_item != null) // Verifica se o tipo de item não é nulo
        {
            _ic.slot = _item.GetComponent<ItemType>().itemType; // Adiciona o item ao inventário
            _ic.SlotImage = _item.GetComponent<ItemType>().itemType.itemSprite; // Adiciona a imagem do item ao inventário
            Destroy(_item); // Destroi o objeto após a interação
            _interactText.text = "";
        }
    }
}
