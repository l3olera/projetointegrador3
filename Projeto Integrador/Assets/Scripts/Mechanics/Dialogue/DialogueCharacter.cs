using UnityEngine;

public class DialogueCharacter : MonoBehaviour
{
    private TriggerSpeech _triggerSpeech = null; // Referência ao scriptable object TriggerSpeech
    [SerializeField] private DialogueControl _dc; // Referência ao script que controla os diálogos
    [SerializeField] private InventoryController _ic; // Referência ao script que controla o inventário
    [SerializeField] private ObjectivesController _oc; // Referência ao script que controla os objetivos

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerSpeech")) // Verifica se o objeto que entrou no trigger é o jogador
        {
            _triggerSpeech = other.GetComponent<TriggerType>().triggerSpeech; // Obtém o TriggerSpeech do objeto

            if (_oc.CompareAct(_triggerSpeech.toAct) && !_triggerSpeech.isTriggered)
            {
                if (!_ic.HasItemById(_triggerSpeech.requiredIdItem))
                {
                    _dc.Speech(_triggerSpeech.dialogueSequence[0].lines); // Passa o array de falas para o DialogueControl
                }
                else
                {
                    _dc.Speech(_triggerSpeech.dialogueSequence[1].lines); // Passa o array de falas para o DialogueControl
                }
                _triggerSpeech.isTriggered = true; // Marca o diálogo como acionado
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TriggerSpeech")) // Verifica se o objeto que saiu do trigger é o jogador
        {
            _triggerSpeech = null; // Reseta o TriggerSpeech
        }
    }
}
