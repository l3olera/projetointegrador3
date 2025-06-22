using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public enum CutscenesName
{
    Start,
    Salem,
    Final
}

public class CutsceneManager : MonoBehaviour
{
    [Header("Cameras para referenciar")]
    [SerializeField] private CinemachineCamera _cmCutsceneStart;
    [SerializeField] private CinemachineCamera _cmCutsceneSalem;
    [SerializeField] private CinemachineCamera _cmCutsceneFinal;
    [SerializeField] private CinemachineCamera _cmFreeLook;

    [Header("Timelines para referenciar")]
    [SerializeField] private PlayableDirector _cutsceneStart;
    [SerializeField] private PlayableDirector _cutsceneSalem;
    [SerializeField] private PlayableDirector _cutsceneFinal;

    [Header("Componente para referenciar")]
    [SerializeField] private PlayerMovement _playerMovement; // ou script de movimento do player
    [SerializeField] private GameObject _hud;
    [SerializeField] private GameObject _cutscenesObj;
    [SerializeField] private SceneLoader _sceneLoader;

    [Header("Atributos da cena final")]
    [SerializeField] private string _nextScene;

    [Header("Atributo da Cutscene")]
    public CutscenesName currentName;
    public static event Action<CutscenesName> OnCutsceneEnd;

    void OnEnable()
    {
        //_cutsceneStart.stopped += OnStartCutsceneEnd;
        _cutsceneSalem.stopped += OnSalemCutsceneEnd;
        _cutsceneFinal.stopped += OnFinalCutsceneEnd;
    }

    void OnDisable()
    {
        //_cutsceneStart.stopped -= OnStartCutsceneEnd;
        _cutsceneSalem.stopped -= OnSalemCutsceneEnd;
        _cutsceneFinal.stopped -= OnFinalCutsceneEnd;
    }

    void Start()
    {
        if (_cutscenesObj.activeSelf)
        {
            _cutscenesObj.SetActive(false);
        }
    }

    public void SelectCutscene(CutscenesName name)
    {
        currentName = name;
        switch (name)
        {
            case CutscenesName.Start:
                StartCutscene(_cmCutsceneStart, _cutsceneStart);
                break;
            case CutscenesName.Salem:
                StartCutscene(_cmCutsceneSalem, _cutsceneSalem);
                break;
            case CutscenesName.Final:
                StartCutscene(_cmCutsceneFinal, _cutsceneFinal);
                break;
        }
    }

    void StartCutscene(CinemachineCamera camCut, PlayableDirector timeline)
    {
        _cutscenesObj.SetActive(true);
        _hud.SetActive(false);
        camCut.Priority = 20;
        _cmFreeLook.Priority = 10;
        _playerMovement.enabled = false;
        timeline.Play();
    }

    void OnStartCutsceneEnd(PlayableDirector dir)
    {
        EndCutscene(_cmCutsceneStart);
    }

    void OnSalemCutsceneEnd(PlayableDirector dir)
    {
        EndCutscene(_cmCutsceneSalem);
    }

    void OnFinalCutsceneEnd(PlayableDirector dir)
    {
        EndCutscene(_cmCutsceneFinal);
    }

    void EndCutscene(CinemachineCamera camCut)
    {
        _hud.SetActive(true);
        camCut.Priority = 10;
        _cmFreeLook.Priority = 20;
        _playerMovement.enabled = true;
        OnCutsceneEnd?.Invoke(currentName);
        _cutscenesObj.SetActive(false);
    }
}