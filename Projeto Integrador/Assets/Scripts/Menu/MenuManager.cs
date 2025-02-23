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
    [Tooltip("Referência ao GameObject popupControls")]
    [SerializeField] private GameObject popupControls;

    void Start()
    {
        //Verifica se créditos foi instanciado e se ele está ativo quando inicia a cena. Assim, escondendo-o caso for verdadeiro
        if(credits != null){
            if(credits.activeSelf){
                credits.SetActive(false);
            }
        }    

        if(popupControls != null){
            if(popupControls.activeSelf){
                popupControls.SetActive(false);
            }
        }
        
    }

    public void ButtonStart(){
        sceneLoader.Transition(nextSceneStart); //Responsável por fazer transição entre a tela de menu e o a cena do jogo
    }
    
    public void ButtonOptions(){

    }

    public void ButtonCredits()
    {
        //Verifica se créditos está instanciado e mostra e esconde os créditos
        if(credits != null){
            if(!credits.activeSelf){
                credits.SetActive(true);
            }else{
                credits.SetActive(false);
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

    public void OpenPopUp(GameObject objectPopUp){
        //Deixa ativo um gameObject que é um popUp
        if(objectPopUp != null){
            if(!objectPopUp.activeSelf){
                objectPopUp.SetActive(true);
            }
        }
    }

    public void ClosePopUp(GameObject objectPopUp){
        //Deixa desativado um gameObject que é um popUp
        if(objectPopUp != null){
            if(objectPopUp.activeSelf){
                objectPopUp.SetActive(false);
            }
        }
    }
}
