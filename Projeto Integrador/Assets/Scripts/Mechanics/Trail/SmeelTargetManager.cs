using UnityEngine;

public class SmellTargetManager : MonoBehaviour
{
    public Transform[] act1Targets; // controle, portão
    public Transform[] act2Targets; // pombos, catatau, salem, lã, salem de novo

    private int currentTargetIndex = 0;
    private Transform[] currentActTargets;

    void Start()
    {
        // Começa com Ato 1
        currentActTargets = act1Targets;
    }

    public Transform GetCurrentTarget() => currentActTargets[currentTargetIndex];

    public void NextTarget()
    {
        currentTargetIndex++;
        if (currentTargetIndex >= currentActTargets.Length)
            currentTargetIndex = currentActTargets.Length - 1;
    }

    public void SwitchToAct2()
    {
        currentActTargets = act2Targets;
        currentTargetIndex = 0;
    }
}

