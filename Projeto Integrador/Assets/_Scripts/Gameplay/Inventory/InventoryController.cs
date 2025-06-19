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

    public static InventoryController Instance { get; private set; }// Propriedade para acessar a instância única do InventoryController

    void Awake()
    {
        if (Instance != null && Instance != this) // Verifica se já existe uma instância
        {
            Destroy(this.gameObject); // Destroi o objeto atual se já houver uma instância
            return; // Sai do método para evitar duplicação
        }
        Instance = this; // Inicializa a instância única do InventoryController

        _slotImageUI.color = new Color(1, 1, 1, 0); // Totalmente invisível (alpha 0)
    }

    // Função chamada quando o valor de slotImage muda
    private void OnSlotImageChanged()
    {
        if (_slotImage == null)
        {
            _slotImageUI.color = new Color(1, 1, 1, 0); // Totalmente invisível (alpha 0)
        }
        else
        {
            _slotImageUI.color = new Color(1, 1, 1, 1); // Totalmente visível (alpha 1)
        }

        _slotImageUI.sprite = _slotImage; // Atualiza a imagem do slot na UI
        _slotImageUI.SetNativeSize(); // Ajusta o tamanho da Image de acordo com a textura origina
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

    public void RemoveItem()
    {
        if (slot != null)
        {
            slot = null; // Remove o item do slot
            SlotImage = null; // Limpa a imagem do slot na UI
        }
    }
}