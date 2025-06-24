using UnityEngine;

public abstract class DialogueTriggerNpc : MonoBehaviour
{
    public DialogueSequence[] dialogueSequence;
    public OccurrencesDialogue dialogueId;

    protected DialogueControl _dc;
    protected SmellTargetManager _smell;
    protected bool[] _dialoguePlayed; // Array para controlar quais diálogos foram reproduzidos
    private TextInteractManager _textInteract; // Referência ao gerenciador de texto de interação
    private InputManager _im; // Referência ao gerenciador de entrada

    private bool _isInRadious;
    [SerializeField] private string _translateName; // Nome da tradução para o texto de interação

    void Awake()
    {
        _dialoguePlayed = new bool[dialogueSequence.Length];
    }

    protected virtual void Start()
    {
        _dc = DialogueControl.Instance;
        _smell = SmellTargetManager.Instance;
        _textInteract = TextInteractManager.Instance; // Obtém a referência ao TextInteractManager
        _im = InputManager.Instance; // Obtém a instância do InputManager
    }

    public void EnableInteraction()
    {
        _isInRadious = true;
        _textInteract.canSetText = true; // Permite que o texto de interação seja definido
    }

    public void DisableInteraction()
    {
        _isInRadious = false;
        _textInteract.canSetText = false; // Impede que o texto de interação seja definido
    }

    void Update()
    {
        if (_isInRadious)
        {
            _textInteract.SetText(LocalizationManager.Instance.GetTranslation(_translateName)); // Obtém a tradução do texto de interação

            if (_im.IsInteractKeyPressed() && _dc.canInteract) // Verifica se a tecla de interação foi pressionada
            {
                TryStartDialogue();
            }
        }
    }

    protected abstract void TryStartDialogue();
}
