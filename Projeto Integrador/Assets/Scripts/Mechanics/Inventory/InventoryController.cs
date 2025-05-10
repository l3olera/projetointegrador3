using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Items slot;
    private Sprite _slotImage; // Campo privado para armazenar o valor atual da imagem do slot
    public Sprite SlotImage
    {
        get => _slotImage; // Retorna o valor atual
        set
        {
            if (_slotImage != value) // Verifica se o valor mudou
            {
                _slotImage = value; // Atualiza o valor
                OnSlotImageChanged(); // Chama a função quando o valor muda
            }
        }
    } // Referência à image do slot

    [SerializeField] private Image _slotImageUI; // Referência à UI do inventário

    void Start()
    {
        ReferenceManager.Instance.inventoryController = this; // Inicializa a referência ao InventoryController no ReferenceManager

    }

    // Função chamada quando o valor de slotImage muda
    private void OnSlotImageChanged()
    {
        _slotImageUI.sprite = _slotImage; // Atualiza a imagem do slot na UI
    }

    public bool HasItemById(int id)
    {
        if (slot != null)
        {
            if (slot.itemID == id)
            {
                return true; // Retorna verdadeiro se o ID do item no slot for igual ao ID procurado
            }
        }
        return false; // Retorna falso se o slot estiver vazio ou não contiver o item exato
    }

    public void RevoveItem()
    {
        if (slot != null)
        {
            slot = null; // Remove o item do slot
            SlotImage = null; // Limpa a imagem do slot na UI
        }
    }
}