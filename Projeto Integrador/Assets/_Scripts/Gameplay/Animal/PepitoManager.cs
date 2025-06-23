using UnityEngine;

public class PepitoManager : MonoBehaviour
{
    [SerializeField] private OccurrencesDialogue occurrenceDialogue; // ID do diálogo atual

    void OnEnable()
    {
        DialogueControl.OnDialogueEnd += CheckDialogue; // Adiciona o evento para desativar o PepitoManager quando o diálogo termina
    }

    void OnDisable()
    {
        DialogueControl.OnDialogueEnd -= CheckDialogue; // Remove o evento ao desativar o PepitoManager
    }

    private void CheckDialogue(OccurrencesDialogue dialogueID)
    {
        if (dialogueID == occurrenceDialogue)
        {
            DisablePepito(); // Desativa o PepitoManager se o diálogo atual for o esperado
        }
    }

    void DisablePepito()
    {
        this.gameObject.SetActive(false); // Método para desativar o PepitoManager manualmente
    }
}
