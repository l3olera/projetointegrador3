using System;

[Serializable]
public class UniqueDialogue
{
    public DialogueSequence[] dialogueSequence; // Array de falas
    public int toAct; // Fala para qual ato ele é
    public int requiredIdItem = -1; // ID do item necessário para continuar o diálogo, se precisar (-1 = não precisa);
    public OccurrencesDialogue occurrenceDialogue; // ID do diálogo atual
}
