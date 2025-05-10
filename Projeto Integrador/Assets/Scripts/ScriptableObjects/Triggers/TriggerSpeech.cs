using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "TriggerSpeech", menuName = "ScriptableObjects/Triggers/TriggerSpeech")]
public class TriggerSpeech : ScriptableObject
{
    public bool isTriggered = false; // Verifica se o diálogo já foi acionado
    public DialogueSequence[] dialogueSequence; // Array de sequências de diálogos
    public int toAct; // Fala para qual ato ele é
    public int requiredIdItem = -1; // ID do item necessário para continuar um próximo dialogo o diálogo (Já vem com -1, representando que não é necessário nenhum item)
}
