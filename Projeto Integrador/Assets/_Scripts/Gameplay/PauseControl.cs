using System;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    [Tooltip("Instancia o canvas do pause")]
    public GameObject pauseCanvas; //Instancia o canvas do pause
    public SceneLoader sceneLoader; //Instancia o sceneLoader
    public String nextScene; //Nome da próxima cena
    bool _isPause = false; //Armazena se o jogo está pausado ou não
    [SerializeField] private PlayerMovement _pm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Garante que o jogo não estará pausado ao iniciar
        EnableCanvas(false);
    }

    //Pausa o jogo e mostra o canva do Pause
    void Pause()
    {
        StopGame();
        EnableCanvas(true);
        _pm.FreeMouse();
    }

    //Despausa o jogo e esconde o canva do Pause
    void UnPause()
    {
        ReturnGame();
        EnableCanvas(false);
        _pm.LockMouse();
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void ReturnGame()
    {
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        if (sceneLoader != null)
        {
            Time.timeScale = 1;
            sceneLoader.Transition(nextScene);
        }
    }

    //Responsavel por mostra ou esconder o canvas
    void EnableCanvas(bool status)
    {
        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(status);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Executa o pause se o jogador pressionar a tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TooglePause();
        }
    }

    public void TooglePause()
    {
        _isPause = !_isPause;

        if (_isPause)
        {
            Pause();
        }
        else
        {
            UnPause();
        }
    }
}
