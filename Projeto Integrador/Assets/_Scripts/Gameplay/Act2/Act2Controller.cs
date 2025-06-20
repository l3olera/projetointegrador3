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
    [SerializeField] private float _transitionFade = 2.5f;
    [SerializeField] private float _transitionToChangeObjects = 2f;

    [Header("Objetos que mudarão para fase 3")]
    [SerializeField] private GameObject _trashCan;
    [SerializeField] private Vector3 _position;
    [SerializeField] private Quaternion _rotation;

    [SerializeField] private GameObject _fenceAct3;

    void OnEnable()
    {
        DialogueControl.OnDialogueEnd += StartTransitionToAct3;
    }

    void OnDisable()
    {
        DialogueControl.OnDialogueEnd += StartTransitionToAct3;
    }

    void StartTransitionToAct3(OccurrencesDialogue dialogueId)
    {
        if (dialogueId == _occurenceDialogue)
        {
            StartCoroutine(MakeTransition());
        }
    }

    IEnumerator MakeTransition()
    {
        _fc.StartFade();
        StartCoroutine(ChangeObjects());
        yield return new WaitForSeconds(_transitionFade);
        _fc.EndFade();
    }

    IEnumerator ChangeObjects()
    {
        yield return new WaitForSeconds(_transitionToChangeObjects);
        _fenceAct3.SetActive(true);
        _trashCan.transform.SetPositionAndRotation(_position, _rotation);
        _playerSpawn.SpawnPlayer(_idSpawn);
    }
}
