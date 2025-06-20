using UnityEngine;

public class TriggerTutorialSmell : MonoBehaviour
{
    private bool _pressedSmellButton = false; // Registra se o botão de cheiro foi pressionado
    private DialogueControl _dc; // Referência ao controle de diálogo
    private InputManager _im; // Referência ao gerenciador de entrada
    [SerializeField] private DialogueSequence[] _dialogueSequence; // Sequência de diálogos para o tutorial de cheiro

    void Start()
    {
        _dc = DialogueControl.Instance;
        _im = InputManager.Instance; // Obtém a instância do InputManager
    }

    void Update()
    {
        if (_im.IsSmellKeyPressed()) // Verifica se a tecla de cheiro foi pressionada
        {
            if (!_pressedSmellButton)
            {
                // Inicia o cheiro se o botão não foi pressionado antes
                _pressedSmellButton = true; // Marca que o botão foi pressionado
                PlayDialogue();
            }
        }
    }

    void PlayDialogue()
    {
        _dc.Speech(_dialogueSequence[0].lines); // Inicia o diálogo após pressionar o botão de cheiro
        Destroy(gameObject); // Destroi o objeto do tutorial após o diálogo
    }
}
