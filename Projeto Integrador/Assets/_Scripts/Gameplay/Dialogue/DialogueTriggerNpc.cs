using UnityEngine;

public abstract class DialogueTriggerNpc : MonoBehaviour
{
    public DialogueSequence[] dialogueSequence;
    public OccurrencesDialogue dialogueId;

    protected DialogueControl _dc;
    protected SmellTargetManager _smell;
    private TextInteractManager _textInteract; // Referência ao gerenciador de texto de interação
    private InputManager _im; // Referência ao gerenciador de entrada

    private bool _canInteract;
    [SerializeField] private string _translateName; // Nome da tradução para o texto de interação

    protected virtual void Start()
    {
        _dc = DialogueControl.Instance;
        _smell = SmellTargetManager.Instance;
        _textInteract = TextInteractManager.Instance; // Obtém a referência ao TextInteractManager
        _im = InputManager.Instance; // Obtém a instância do InputManager
    }

    public void EnableInteraction()
    {
        _canInteract = true;
        _textInteract.canSetText = true; // Permite que o texto de interação seja definido
    }

    public void DisableInteraction()
    {
        _canInteract = false;
        _textInteract.canSetText = false; // Impede que o texto de interação seja definido
    }

    void Update()
    {
        if (_canInteract)
        {
            _textInteract.SetText(LocalizationManager.Instance.GetTranslation(_translateName)); // Obtém a tradução do texto de interação

            if (_im.IsInteractKeyPressed()) // Verifica se a tecla de interação foi pressionada
            {
                TryStartDialogue();
            }
        }
    }

    protected abstract void TryStartDialogue();
}
