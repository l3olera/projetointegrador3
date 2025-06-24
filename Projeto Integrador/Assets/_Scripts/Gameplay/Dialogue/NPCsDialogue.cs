public class NPCsDialogue : DialogueTriggerNpc
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TryStartDialogue()
    {
        _dc.DefineOccurrenceDialogue(dialogueId);

        for (int i = 0; i < dialogueSequence.Length; i++)
        {
            _dc.Speech(dialogueSequence[i].lines);
            if (!_dialoguePlayed[i])
            {
                _smell.NextTarget(); // Avança para o próximo alvo de cheiro após o diálogo
            }
        }
    }
}
