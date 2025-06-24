using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator _anim; // Referência à porta que será aberta
    [SerializeField] private int targetPuzzleID = 1; // ID do puzzle que abre esta porta

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
        _anim = GetComponent<Animator>(); // Obtém a referência ao Animator da porta
    }

    void CheckPuzzle(int puzzleID)
    {
        if (puzzleID == targetPuzzleID)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        if (_anim != null)
        {
            _anim.SetBool("canOpen", true); // Abre a porta
        }
    }
}
