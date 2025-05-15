using UnityEngine;

public class PlayerSmellInput : MonoBehaviour
{
    public SmellTrail trail;
    public SmellTargetManager manager;

    void Update()
    {
        if (trail == null)
        {
            trail = ReferenceManager.Instance.smellTrail;
        }

        if (manager == null)
        {
            manager = ReferenceManager.Instance.smellTargetManager;
        }

        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V))
        {
            trail.player = transform;
            trail.target = manager.GetCurrentTarget();
            trail.GenerateTrail();
        }
    }
}
