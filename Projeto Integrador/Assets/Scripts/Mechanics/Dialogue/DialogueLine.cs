using UnityEngine.Localization;

[System.Serializable]
public class DialogueLine
{
    public Animal actor; // Ator que fala
    public LocalizedString[] speechText; // Texto que o ator fala
}
