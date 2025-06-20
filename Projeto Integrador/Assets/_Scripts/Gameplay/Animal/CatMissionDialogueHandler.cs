public class CatMissionDialogueHandler : DialogueTriggerNpc
{
    public int requiredItemId = 2; // ID do item necessário para mudar o diálogo
    private bool[] _dialoguePlayed; // Array para controlar quais diálogos foram reproduzidos
    private InventoryController _ic;
    private ObjectivesController _oc;

    void Awake()
    {
        _dialoguePlayed = new bool[dialogueSequence.Length];
    }

    protected override void Start()
    {
        base.Start();
        _ic = InventoryController.Instance;
        _oc = ObjectivesController.Instance;
    }

    protected override void TryStartDialogue()
    {
        if (_ic.HasItemById(requiredItemId) && !_dialoguePlayed[1])
        {
            _dc.currentOccurrenceDialogue = dialogueId;
            _dc.Speech(dialogueSequence[1].lines);
            _dialoguePlayed[1] = true;
            _smell.NextTarget();
            _ic.RemoveItem();
            _oc.IncreaseActIndex();

        }
        else if (!_ic.HasItemById(requiredItemId) && !_dialoguePlayed[0])
        {
            _dc.Speech(dialogueSequence[0].lines);
            _dialoguePlayed[0] = true;
            _smell.NextTarget();
        }
    }
}