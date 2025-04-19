using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class MenuManager : MonoBehaviour
{
    [Header("Cenas")]
    [Tooltip("Cena que o jogadora irá ao clicar em Jogar")]
    [SerializeField] private string _nextSceneStart;

    [Header("Referências de classes e objetos")]
    [Tooltip("Referência a classe sceneLoader")]
    [SerializeField] private SceneLoader _sceneLoader;
    [Tooltip("Referência ao GameObject credits")]
    [SerializeField] private GameObject _credits;
    [Tooltip("Referência ao GameObject popupControls")]
    [SerializeField] private GameObject _popupControls;
    [Tooltip("Referência ao GameObject popupOptions")]
    [SerializeField] private GameObject _popupOptions;
    [Tooltip("Referência ao Image do button_English")]
    [SerializeField] private Image _bgEnglish;
    [Tooltip("Referência ao Image do button_Portuguese")]
    [SerializeField] private Image _bgPortuguese;

    void Start()
    {
        //Verifica se créditos foi instanciado e se ele está ativo quando inicia a cena. Assim, escondendo-o caso for verdadeiro
        if(_credits != null){
            if(_credits.activeSelf){
                _credits.SetActive(false);
                _credits.GetComponent<TextOscillator>().enabled = false;
            }
        }    

        if(_popupControls != null){
            if(_popupControls.activeSelf){
                _popupControls.SetActive(false);
            }
        }
        
        if(_popupOptions != null){
            if(_popupOptions.activeSelf){
                _popupOptions.SetActive(false);
            }
        }

        //Verifica e aplica o idioma salvo
        int savedLanguage = PlayerPrefs.GetInt("Language", 0); //0 - Português, 1 - Inglês (Se não existir, usa 0 como padrão)
        SetLanguage(savedLanguage);
    }

    public void ButtonStart(){
        _sceneLoader.Transition(_nextSceneStart); //Responsável por fazer transição entre a tela de menu e o a cena do jogo
    }

    public void ButtonCredits()
    {
        //Verifica se créditos está instanciado e mostra e esconde os créditos
        if(_credits != null){
            if(!_credits.activeSelf){
                _credits.SetActive(true);
                _credits.GetComponent<TextOscillator>().enabled = true;
            }else{
                _credits.SetActive(false);
                _credits.GetComponent<TextOscillator>().enabled = false;
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
        _bgEnglish.enabled = languageIndex == 1;
        _bgPortuguese.enabled = languageIndex == 0;
    }
}
