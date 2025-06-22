using UnityEngine;

public class TriggerTutorialSmell : MonoBehaviour
{
    private bool _pressedSmellButton = false; // Registra se o botão de cheiro foi pressionado
    private DialogueControl _dc; // Referência ao controle de diálogo
    private InputManager _im; // Referência ao gerenciador de entrada
    private ObjectivesController _oc;
    [SerializeField] private DialogueSequence[] _dialogueSequence; // Sequência de diálogos para o tutorial de cheiro
    [SerializeField] private int _designatedAct = 1;

    void Start()
    {
        _dc = DialogueControl.Instance;
        _im = InputManager.Instance; // Obtém a instância do InputManager
        _oc = ObjectivesController.Instance;
    }

    void Update()
    {
        if (!_oc.CompareAct(_designatedAct))
        {
            Destroy(this);
        }

        if (_im.IsSmellKeyPressed()) // Verifica se a tecla de cheiro foi pressionada
        {
            if (!_pressedSmellButton)
            {
                // Inicia o cheiro se o botão não foi pressionado antes
                _pressedSmellButton = true; // Marca que o botão foi pressionado
                PlayDialogue();
            }
        }
    }

    void PlayDialogue()
    {
        _dc.Speech(_dialogueSequence[0].lines); // Inicia o diálogo após pressionar o botão de cheiro
        Destroy(this); // Destroi o objeto do tutorial após o diálogo
    }
}
