using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int targetPuzzleID = 2; // ID do puzzle que ativa o dialogo
    [SerializeField] private DialogueSequence _dialogueSequenceEndPuzzleAct3; // Referência ao diálogo que será ativado
    private DialogueControl _dialogueControl; // Referência ao script que controla os diálogos
    private SmellTargetManager _smellTargetManager;

    void OnEnable()
    {
        LeverPuzzle.OnPuzzleSolved += CheckPuzzle;
    }

    void OnDisable()
    {
        LeverPuzzle.OnPuzzleSolved -= CheckPuzzle;
    }

    void Update()
    {
        if (_dialogueControl == null)
        {
            _dialogueControl = ReferenceManager.Instance.dialogueControl; // Obtém a referência ao DialogueControl do ReferenceManager
        }

        if (_smellTargetManager == null)
        {
            _smellTargetManager = ReferenceManager.Instance.smellTargetManager;
        }
    }

    void CheckPuzzle(int puzzleID)
    {
        if (puzzleID == targetPuzzleID)
        {
            PlayDialogueEndPuzzle();

            if (_smellTargetManager != null)
            {
                _smellTargetManager.NextTarget(); // Avança para o próximo alvo
            }
        }
    }

    void PlayDialogueEndPuzzle()
    {
        if (_dialogueControl != null && _dialogueSequenceEndPuzzleAct3 != null)
        {
            _dialogueControl.Speech(_dialogueSequenceEndPuzzleAct3.lines);
        }
    }
}
