using UnityEngine;

public class DialogueCharacter : MonoBehaviour
{
    private UniqueDialogueTrigger _triggerSpeech = null; // Referência ao scriptable object TriggerSpeech
    [SerializeField] private DialogueControl _dc; // Referência ao script que controla os diálogos
    [SerializeField] private InventoryController _ic; // Referência ao script que controla o inventário
    [SerializeField] private ObjectivesController _oc; // Referência ao script que controla os objetivos
    [SerializeField] private AudioSource _barkAudioSource; // Referência ao audioSource responsável por emitir o latido de Lakshmi

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerSpeech")) // Verifica se o objeto que entrou no trigger é a caixa de trigger
        {
            _triggerSpeech = other.GetComponent<UniqueDialogueTrigger>(); // Obtém o TriggerSpeech do objeto

            if (_oc.CompareAct(_triggerSpeech.dialogueData.toAct) && !_triggerSpeech.hasTriggered)
            {
                _dc.DefineOccurrenceDialogue(_triggerSpeech.dialogueData.occurrenceDialogue); // Define o diálogo atual
                if (!_ic.HasItemById(_triggerSpeech.dialogueData.requiredIdItem))
                {
                    _dc.Speech(_triggerSpeech.dialogueData.dialogueSequence[0].lines); // Passa o array de falas para o DialogueControl
                }
                else
                {

                    _dc.Speech(_triggerSpeech.dialogueData.dialogueSequence[1].lines); // Passa o array de falas para o DialogueControl
                }
                _triggerSpeech.hasTriggered = true; // Define que o diálogo já foi ativado
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TriggerSpeech")) // Verifica se o objeto que saiu do trigger é a caixa de trigger
        {
            _triggerSpeech = null; // Reseta o TriggerSpeech
        }
    }
}