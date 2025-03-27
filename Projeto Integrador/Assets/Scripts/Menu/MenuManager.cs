using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

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
    [Tooltip("Referência ao GameObject popupOptions")]
    [SerializeField] private GameObject popupOptions;
    [Tooltip("Referência ao Image do button_English")]
    [SerializeField] private Image bgEnglish;
    [Tooltip("Referência ao Image do button_Portuguese")]
    [SerializeField] private Image bgPortuguese;

    void Start()
    {
        //Verifica se créditos foi instanciado e se ele está ativo quando inicia a cena. Assim, escondendo-o caso for verdadeiro
        if(credits != null){
            if(credits.activeSelf){
                credits.SetActive(false);
                credits.GetComponent<TextOscillator>().enabled = false;
            }
        }    

        if(popupControls != null){
            if(popupControls.activeSelf){
                popupControls.SetActive(false);
            }
        }
        
        if(popupOptions != null){
            if(popupOptions.activeSelf){
                popupOptions.SetActive(false);
            }
        }

        //Verifica e aplica o idioma salvo
        int savedLanguage = PlayerPrefs.GetInt("Language", 0); //0 - Português, 1 - Inglês (Se não existir, usa 0 como padrão)
        SetLanguage(savedLanguage);
    }

    public void ButtonStart(){
        sceneLoader.Transition(nextSceneStart); //Responsável por fazer transição entre a tela de menu e o a cena do jogo
    }

    public void ButtonCredits()
    {
        //Verifica se créditos está instanciado e mostra e esconde os créditos
        if(credits != null){
            if(!credits.activeSelf){
                credits.SetActive(true);
                credits.GetComponent<TextOscillator>().enabled = true;
            }else{
                credits.SetActive(false);
                credits.GetComponent<TextOscillator>().enabled = false;
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

    public void SetLanguage(int languageIndex){
        // Define o idioma na Unity Localization
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];

        //Salva a escolha do jogador
        PlayerPrefs.SetInt("Language", languageIndex);
        PlayerPrefs.Save();

        //Atualiza a aparência dos botões
        bgEnglish.enabled = languageIndex == 1;
        bgPortuguese.enabled = languageIndex == 0;
    }
}
