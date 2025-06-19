using UnityEngine;

public class TruckController : MonoBehaviour
{
    [SerializeField] private int targetPuzzleID = 2; // ID do puzzle que ativa o caminhão
    [SerializeField] private GameObject lights; // Referência ao GameObject que contém as luzes do caminhão
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
            ReleasePassage();
        }
    }

    void ReleasePassage()
    {
        if (_anim != null)
        {
            _anim.SetTrigger("PlayMove");
            lights.SetActive(false); // Desativa as luzes do caminhão
        }
    }
}
