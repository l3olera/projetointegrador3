using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int targetPuzzleID = 2; // ID do puzzle que ativa o dialogo
    [SerializeField] private UniqueDialogue _dialogueEndPuzzleAct3; // Referência ao diálogo que será ativado
    [SerializeField] private ItemType _pepitoItem; // Referência ao tipo de item que será verificado no inventário
    private InventoryController _ic; // Referência ao InventoryController
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

    void Start()
    {
        _dialogueControl = DialogueControl.Instance; // Obtém a referência ao DialogueControl
        _ic = InventoryController.Instance; // Obtém a referência ao InventoryController
        _smellTargetManager = SmellTargetManager.Instance; // Obtém a referência ao SmellTargetManager
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
        if (_dialogueControl != null && _dialogueEndPuzzleAct3 != null)
        {
            _dialogueControl.DefineOccurrenceDialogue(_dialogueEndPuzzleAct3.occurrenceDialogue); // Define o diálogo atual
            _dialogueControl.Speech(_dialogueEndPuzzleAct3.dialogueSequence[0].lines); // Inicia o diálogo com a primeira sequência de falas

            if (_pepitoItem != null)
            {
                _ic.slot = _pepitoItem.itemType; // Adiciona o item ao inventário
                _ic.SlotImage = _pepitoItem.itemType.itemSprite; // Adiciona o sprite do item no inventário
            }
        }
    }

}
