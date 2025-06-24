using TMPro;
using UnityEngine;

public class TextInteractManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textInteract; // Referência ao componente de texto de interação
    public bool canSetText = false; // Controla se é possível mudar o texto de interação
    public static TextInteractManager Instance { get; private set; } // Instância única do TextInteractManager

    void Awake()
    {
        if (Instance != null && Instance != this) // Verifica se já existe uma instância
        {
            Destroy(this.gameObject); // Destroi o objeto atual se já houver uma instância
            return; // Sai do método para evitar duplicação
        }
        Instance = this; // Define a instância atual como a única instância
    }

    void Update()
    {
        if (_textInteract.text != string.Empty && !canSetText)
        {
            _textInteract.text = string.Empty; // Limpa o texto se não for possível definir
        }
    }

    public void SetText(params string[] texts)
    {
        if (canSetText)
        {
            _textInteract.text = string.Join(" ", texts);
        }
    }
}
