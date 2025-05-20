using UnityEngine;

public class DialogueCharacter : MonoBehaviour
{
    private UniqueDialogueTrigger _triggerSpeech = null; // Referência ao scriptable object TriggerSpeech
    [SerializeField] private DialogueControl _dc; // Referência ao script que controla os diálogos
    [SerializeField] private InventoryController _ic; // Referência ao script que controla o inventário
    [SerializeField] private ObjectivesController _oc; // Referência ao script que controla os objetivos

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerSpeech")) // Verifica se o objeto que entrou no trigger é a caixa de trigger
        {
            _triggerSpeech = other.GetComponent<UniqueDialogueTrigger>(); // Obtém o TriggerSpeech do objeto

            if (_oc.CompareAct(_triggerSpeech.toAct) && !_triggerSpeech.hasTriggered)
            {
                if (!_ic.HasItemById(_triggerSpeech.requiredIdItem))
                {
                    _dc.Speech(_triggerSpeech.dialogueSequence[0].lines); // Passa o array de falas para o DialogueControl
                }
                else
                {
                    _dc.Speech(_triggerSpeech.dialogueSequence[1].lines); // Passa o array de falas para o DialogueControl
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
