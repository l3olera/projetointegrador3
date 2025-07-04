using System.Collections;
using UnityEngine;

public class Act2Controller : MonoBehaviour
{
    [Header("Componentes a ser referenciados")]
    [SerializeField] private FadeController _fc;
    [SerializeField] private PlayerSpawn _playerSpawn;

    [Header("Atributos")]
    [SerializeField] private OccurrencesDialogue _occurenceDialogue;
    [SerializeField] private int _idSpawn;

    [Header("Objetos que mudarão para fase 3")]
    [SerializeField] private GameObject _trashCan;
    [SerializeField] private Vector3 _position;
    [SerializeField] private Quaternion _rotation;

    [SerializeField] private GameObject _fenceAct3;
    [SerializeField] private GameObject _salem;
    [SerializeField] private CatMissionDialogueHandler _catMission;
    [SerializeField] private CutsceneManager _cutscene;
    [SerializeField] private CutscenesName _cutsceneName;

    void OnEnable()
    {
        DialogueControl.OnDialogueEnd += StartTransitionToAct3;
    }

    void OnDisable()
    {
        DialogueControl.OnDialogueEnd -= StartTransitionToAct3;
    }

    void StartTransitionToAct3(OccurrencesDialogue dialogueId)
    {
        if (dialogueId == _occurenceDialogue)
        {
            _cutscene.SelectCutscene(_cutsceneName);
            StartCoroutine(MakeTransition());
        }
    }

    IEnumerator MakeTransition()
    {
        yield return new WaitForSeconds(_fc.transitionDuration);
        ChangeObjects();
    }

    void ChangeObjects()
    {
        _playerSpawn.SpawnPlayer(_idSpawn);
        _fenceAct3.SetActive(true);
        _trashCan.transform.SetPositionAndRotation(_position, _rotation);
        _catMission.DisableInteraction();
        _salem.SetActive(false);
    }
}
