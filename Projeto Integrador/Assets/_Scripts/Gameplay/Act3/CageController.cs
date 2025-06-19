using UnityEngine;

public class CageController : MonoBehaviour
{
    [SerializeField] private int targetPuzzleID = 2; // ID do puzzle que ativa a jaula
    [SerializeField] private Animator _anim;

    void OnEnable()
    {
        LeverPuzzle.OnPuzzleSolved += CheckPuzzle;
    }

    void OnDisable()
    {
        LeverPuzzle.OnPuzzleSolved -= CheckPuzzle;
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
            _anim.SetTrigger("OpenCage");
        }
    }
}
