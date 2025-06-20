using UnityEngine;

public class EndGameController : MonoBehaviour
{
    [Header("Componentes referenciados")]
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private GateController _gateController;
    private ObjectivesController _oc;
    private DialogueControl _dc;

    [Header("Atributos do último dialogo")]
    [SerializeField] private UniqueDialogue _dialogueUnique;
    [SerializeField] private OccurrencesDialogue _occurenceDialogue;

    [Header("Troca de cena")]
    [SerializeField] private string _sceneName;

    void OnEnable()
    {
        DialogueControl.OnDialogueEnd += EndGame;
    }

    void OnDisable()
    {
        DialogueControl.OnDialogueEnd -= EndGame;
    }

    void Start()
    {
        _oc = ObjectivesController.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (_oc.CompareAct(_dialogueUnique.toAct))
        {
            _gateController.CloseGate();
            _dc.DefineOccurrenceDialogue(_dialogueUnique.occurrenceDialogue);
            _dc.Speech(_dialogueUnique.dialogueSequence[0].lines);
        }
    }

    void EndGame(OccurrencesDialogue dialogueId)
    {
        if (dialogueId == _occurenceDialogue)
        {
            Debug.LogWarning("ACABOU!!!!");
            //_sceneLoader.Transition(_sceneName);
        }
    }
}