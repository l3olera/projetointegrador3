using UnityEngine;

public class DialogueTriggerNpc : MonoBehaviour
{
    public DialogueSequence[] dialogueSequence;
    public int requiredItemId = -1;
    public OccurrencesDialogue dialogueId;

    private DialogueControl _dc;
    private InventoryController _ic;
    private ObjectivesController _oc;
    private SmellTargetManager _smell;
    private TextInteractManager _textInteract; // Referência ao gerenciador de texto de interação
    private bool[] _dialoguePlayed;
    private bool _canInteract;
    [SerializeField] private string _translateName; // Nome da tradução para o texto de interação

    void Awake()
    {
        _dialoguePlayed = new bool[dialogueSequence.Length];

        for (int i = 0; i < _dialoguePlayed.Length; i++)
        {
            _dialoguePlayed[i] = false;
        }
    }

    void Start()
    {
        _dc = DialogueControl.Instance;
        _ic = InventoryController.Instance;
        _oc = ObjectivesController.Instance;
        _smell = SmellTargetManager.Instance;
        _textInteract = TextInteractManager.Instance; // Obtém a referência ao TextInteractManager
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
        if (_canInteract && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z)))
        {
            _textInteract.SetText(LocalizationManager.Instance.GetTranslation(_translateName)); // Obtém a tradução do texto de interação

            TryStartDialogue();
        }
    }
    private void TryStartDialogue()
    {
        _dc.DefineOccurrenceDialogue(dialogueId);

        if (!_ic.HasItemById(requiredItemId))
        {
            _dc.Speech(dialogueSequence[0].lines);
            if (!_dialoguePlayed[0])
            {
                _dialoguePlayed[0] = true;
                _smell.NextTarget();
            }
        }
        else
        {
            _dc.Speech(dialogueSequence[1].lines);
            if (!_dialoguePlayed[1])
            {
                _dialoguePlayed[1] = true;
                _smell.NextTarget();
            }

            _ic.RemoveItem();
            _oc.IncreaseActIndex();
        }
    }
}
