using UnityEngine;

public class Act1Controller : MonoBehaviour
{
    [Header("Componentes referenciados")]
    [SerializeField] private CutsceneManager _cutManager;
    [SerializeField] private PlayerController _player;

    [Header("Atributo Cutscene")]
    [SerializeField] private CutscenesName _cutName;

    void OnEnable()
    {
        CutsceneManager.OnCutsceneEnd += StartGameplay;
    }

    void OnDisable()
    {
        CutsceneManager.OnCutsceneEnd -= StartGameplay;
    }

    void Start()
    {
        _player.ToogleDialogueCharacter(false);
    }

    void StartGameplay(CutscenesName _name)
    {
        if (_name == _cutName)
        {
            _player.ToogleDialogueCharacter(true);
        }
    }


}