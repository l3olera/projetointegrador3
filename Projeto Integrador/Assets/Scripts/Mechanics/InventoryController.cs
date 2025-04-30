using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Items slot; // Prefab do slot do inventário

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

    // Função chamada quando o valor de slotImage muda
    private void OnSlotImageChanged()
    {
        _slotImageUI.sprite = _slotImage; // Atualiza a imagem do slot na UI
    }
}