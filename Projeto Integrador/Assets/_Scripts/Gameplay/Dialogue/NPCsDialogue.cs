public class NPCsDialogue : DialogueTriggerNpc
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void TryStartDialogue()
    {
        _dc.DefineOccurrenceDialogue(dialogueId);

        foreach (var dialogue in dialogueSequence)
        {
            _dc.Speech(dialogue.lines);
            _smell.NextTarget(); // Avança para o próximo alvo de cheiro após o diálogo
            return; // Sai do método após iniciar o diálogo
        }
    }
}
