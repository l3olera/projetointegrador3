using UnityEngine;

public class DialogueTextInteractManager : MonoBehaviour
{
    public bool canChangeText = false; // Controla se o jogador pode mudar o texto de interação

    void Start()
    {
        ReferenceManager.Instance.dialogueTextInteractManager = this; // Define esta instância como o gerenciador de texto de interação
    }
}
