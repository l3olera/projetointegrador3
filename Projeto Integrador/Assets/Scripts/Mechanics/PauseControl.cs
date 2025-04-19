using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
    [Tooltip("Instancia o canvas do pause")]
    public GameObject pauseCanvas; //Instancia o canvas do pause
    public SceneLoader sceneLoader; //Instancia o sceneLoader
    public String nextScene; //Nome da próxima cena
    bool _isPause = false; //Armazena se o jogo está pausado ou não

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Garante que o jogo não estará pausado ao iniciar
        EnableCanvas(false);
    }

    //Pausa o jogo e mostra o canva do Pause
    void Pause(){
        Time.timeScale = 0;

        EnableCanvas(true);
    }

    //Despausa o jogo e esconde o canva do Pause
    void UnPause(){
        Time.timeScale = 1;

        EnableCanvas(false);
    }

    public void BackToMenu()
    {
        if(sceneLoader != null){
            Time.timeScale = 1;
            sceneLoader.Transition(nextScene);
        }
    }

    //Responsavel por mostra ou esconder o canvas
    void EnableCanvas(bool status){
        if(pauseCanvas != null){
            pauseCanvas.SetActive(status);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Executa o pause se o jogador pressionar a tecla ESC
        if(Input.GetKeyDown(KeyCode.Escape)){
            TooglePause();
        }
    }

    public void TooglePause(){
        _isPause = !_isPause;

        if(_isPause){
            Pause();
        }else {
            UnPause();
        }
    }
}
