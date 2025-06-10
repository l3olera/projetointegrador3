using TMPro;
using UnityEngine;

public class TextInteractManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textInteract; // Referência ao componente de texto de interação
    public bool canSetText = false; // Controla se é possível mudar o texto de interação

    void Start()
    {
        ReferenceManager.Instance.textInteractManager = this; // Define esta instância como o gerenciador de texto de interação
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
