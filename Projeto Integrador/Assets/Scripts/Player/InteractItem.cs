using Unity.VisualScripting;
using UnityEngine;

public class InteractItem : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject _interactText; // Referência ao ícone de interação
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
           _interactText.SetActive(true); // Ativa o ícone de interação quando o jogador entra na área de colisão
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            _item = null; // Limpa o tipo de item quando o jogador sai da área de colisão
            isInteractable = false; // Define que o objeto não é mais interagível quando o jogador sai da área de colisão
            _interactText.SetActive(false); // Desativa o ícone de interação quando o jogador sai da área de colisão
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
            _ic.slotImage = _item.GetComponent<ItemType>().itemType.itemSprite; // Adiciona a imagem do item ao inventário
            Destroy(_item); // Destroi o objeto após a interação
        }
    }
}
