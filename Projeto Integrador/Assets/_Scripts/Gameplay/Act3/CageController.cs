using UnityEngine;

public class CageController : MonoBehaviour
{
    [SerializeField] private int targetPuzzleID = 2; // ID do puzzle que ativa a jaula
    private Animator _anim;

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
        _anim = GetComponent<Animator>();
    }

    void CheckPuzzle(int puzzleID)
    {
        if (puzzleID == targetPuzzleID)
        {
            OpenCage();
        }
    }

    void OpenCage()
    {
        if (_anim != null)
        {
            _anim.SetTrigger("Open");
        }
    }
}
