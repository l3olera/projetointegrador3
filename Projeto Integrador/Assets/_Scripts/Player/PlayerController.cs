using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private DialogueCharacter _dialogueCharacter;

    void Start()
    {
        _dialogueCharacter = GetComponent<DialogueCharacter>();
    }

    public void ToogleDialogueCharacter(bool status)
    {
        _dialogueCharacter.enabled = status;
    }
}