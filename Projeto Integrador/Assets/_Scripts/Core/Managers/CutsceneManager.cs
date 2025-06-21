using Unity.Cinemachine;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _cmCutscene;
    [SerializeField] private CinemachineCamera _cmFreeLook;
    [SerializeField] private PlayerMovement _playerMovement; // ou script de movimento do player

    public void StartCutscene()
    {
        _cmCutscene.Priority = 20;
        _cmFreeLook.Priority = 10;
        _playerMovement.enabled = false;
    }

    public void EndCutscene()
    {
        _cmCutscene.Priority = 10;
        _cmFreeLook.Priority = 20;
        _playerMovement.enabled = true;
    }
}