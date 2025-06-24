public class CatMissionDialogueHandler : DialogueTriggerNpc
{
    public int requiredItemId = 2; // ID do item necessário para mudar o diálogo
    private InventoryController _ic;
    private ObjectivesController _oc;


    protected override void Start()
    {
        base.Start();
        _ic = InventoryController.Instance;
        _oc = ObjectivesController.Instance;
    }

    protected override void TryStartDialogue()
    {
        if (_ic.HasItemById(requiredItemId))
        {
            _dc.Speech(dialogueSequence[1].lines);
            _dc.currentOccurrenceDialogue = dialogueId;
            if (!_dialoguePlayed[1])
            {
                _dialoguePlayed[1] = true;
                _oc.IncreaseActIndex();
                _ic.RemoveItem();
                _smell.NextTarget();
            }
        }
        else if (!_ic.HasItemById(requiredItemId))
        {
            _dc.Speech(dialogueSequence[0].lines);

            if (!_dialoguePlayed[0])
            {
                _dialoguePlayed[0] = true;
                _smell.NextTarget();
            }
        }
    }
}