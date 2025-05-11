using UnityEngine;

public class PlayerSmellInput : MonoBehaviour
{
    public SmellTrail trail;
    public SmellTargetManager manager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V))
        {
            trail.player = transform;
            trail.target = manager.GetCurrentTarget();
            trail.GenerateTrail();
        }
    }
}
