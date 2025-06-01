using UnityEngine;

public class UniqueDialogueTrigger : MonoBehaviour
{
    public DialogueSequence[] dialogueSequence; // Array de falas
    public int toAct; // Fala para qual ato ele é
    public int requiredIdItem = -1; // ID do item necessário para continuar o diálogo, se precisar (-1 = não precisa);
    public bool hasTriggered = false; // Verifica se o diálogo já foi ativado;
}
