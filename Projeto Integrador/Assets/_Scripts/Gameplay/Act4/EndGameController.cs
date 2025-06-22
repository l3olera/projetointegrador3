using System.Collections;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    [Header("Componentes referenciados")]
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private GateController _gateController;
    [SerializeField] private PlayerMovement _player;
    private ObjectivesController _oc;
    private DialogueControl _dc;

    [Header("Atributos do último dialogo")]
    [SerializeField] private UniqueDialogue _dialogueUnique;
    [SerializeField] private OccurrencesDialogue _occurenceDialogue;

    [Header("Troca de cena")]
    [SerializeField] private string _sceneName;

    [Header("Atributos Cutscene")]
    [SerializeField] private CutsceneManager _cutscene;
    [SerializeField] private CutscenesName _cutName;
    [SerializeField] private FadeController _fade;

    void OnEnable()
    {
        DialogueControl.OnDialogueEnd += EndGame;
        CutsceneManager.OnCutsceneEnd += GoCredits;
    }

    void OnDisable()
    {
        DialogueControl.OnDialogueEnd -= EndGame;
        CutsceneManager.OnCutsceneEnd -= GoCredits;
    }

    void Start()
    {
        _dc = DialogueControl.Instance;
        _oc = ObjectivesController.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (_oc.CompareAct(_dialogueUnique.toAct))
        {
            //_gateController.CloseGate();
            _dc.DefineOccurrenceDialogue(_dialogueUnique.occurrenceDialogue);
            _dc.Speech(_dialogueUnique.dialogueSequence[0].lines);
        }
    }

    void EndGame(OccurrencesDialogue dialogueId)
    {
        if (dialogueId == _occurenceDialogue)
        {
            _fade.StartFade();
            StartCoroutine(FadeTransition());
        }
    }

    IEnumerator FadeTransition()
    {
        yield return new WaitForSeconds(_fade.transitionDuration);
        _cutscene.SelectCutscene(_cutName);
    }

    void GoCredits(CutscenesName _name)
    {
        if (_name == _cutName)
        {
            _player.FreeMouse();
            _sceneLoader.Transition(_sceneName);
        }

    }
}