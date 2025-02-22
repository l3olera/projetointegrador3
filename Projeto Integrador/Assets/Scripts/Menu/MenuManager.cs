using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Cenas")]
    [Tooltip("Cena que o jogadora irá ao clicar em Jogar")]
    [SerializeField] private string nextSceneStart;

    [Header("Referências de classes e objetos")]
    [Tooltip("Referência a classe sceneLoader")]
    [SerializeField] private SceneLoader sceneLoader;
    [Tooltip("Referência ao GameObject credits")]
    [SerializeField] private GameObject credits;

    void Start()
    {
        //Verifica se créditos foi instanciado e se ele está ativo quando inicia a cena. Assim, escondendo-o caso for verdadeiro
        if(credits != null){
            if(credits.activeSelf){
                credits.SetActive(false);
            }
        }    
    }

    public void ButtonStart(){
        sceneLoader.Transition(nextSceneStart); //Responsável por fazer transição entre a tela de menu e o a cena do jogo
    }

    public void ButtonControls(){

    }
    
    public void ButtonOptions(){

    }

    public void ButtonCredits()
    {
        //Verifica se créditos está instanciado e mostra e esconde os créditos
        if(credits != null){
            if(credits.activeSelf){
                credits.SetActive(false);
            }else{
                credits.SetActive(true);
            }
        }    
    }

    public void ButtonExit(){
        // Exibe uma mensagem de log quando estamos no Editor e sai da aplicação na build
        #if UNITY_EDITOR
            Debug.Log("Jogo encerrado (simulação no Editor)");
        #else
            Application.Quit();
        #endif
    }
}
