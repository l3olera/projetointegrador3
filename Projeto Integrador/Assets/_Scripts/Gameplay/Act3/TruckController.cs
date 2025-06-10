using UnityEngine;

public class TruckController : MonoBehaviour
{
    [SerializeField] private int targetPuzzleID = 2; // ID do puzzle que ativa o caminhão
    private Animator _anim;
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
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (_smellTargetManager == null)
        {
            _smellTargetManager = ReferenceManager.Instance.smellTargetManager;
        }
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
            if (_smellTargetManager != null)
            {
                _smellTargetManager.NextTarget(); // Avança para o próximo alvo
            }
        }
    }
}
