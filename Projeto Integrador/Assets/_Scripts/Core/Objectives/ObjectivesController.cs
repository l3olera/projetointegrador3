using UnityEngine;
using TMPro;
using UnityEngine.Localization;

public class ObjectivesController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _objectivesText; // Referência ao texto de objetivos
    [SerializeField] private LocalizedString[] _objectivesTranslate; // Referência ao texto traduzido dos objetivos

    //RETIRAR ISSO APÓS A DEMO/ALPHA

    [Header("Demo/Alpha Texts")]
    [SerializeField] private TextMeshProUGUI _textDemo1;
    [SerializeField] private TextMeshProUGUI _textDemo2; // Referência ao texto de demonstração
    [SerializeField] private TextMeshProUGUI _textPause;
    [SerializeField] private PauseControl _pc;

    public int CurrentObjective { get; private set; } // variável privada que armazena o índice do objetivo atual

    void Start()
    {
        ReferenceManager.Instance.objectivesController = this; // Inicializa a referência ao ObjectivesController no ReferenceManager

        CurrentObjective = 1; // Inicializa o índice do objetivo atual como 1
        UpdateObjectivesText(); // Chama a função para atualizar o texto dos objetivos ao iniciar o jogo   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) // Verifica se a tecla ESC foi pressionada
        {
            IncreaseActIndex();
        }
    }

    void UpdateObjectivesText()
    {
        if (CurrentObjective <= _objectivesTranslate.Length) // Verifica se o índice do objetivo atual é menor que o tamanho do array de objetivos
        {
            _objectivesTranslate[CurrentObjective - 1].GetLocalizedStringAsync().Completed += handle => // Obtém o texto traduzido
            {
                _objectivesText.text = string.Format(handle.Result); // Atualiza o texto de objetivos
            };
        }
    }

    public void IncreaseActIndex()
    {
        CurrentObjective++; // Incrementa o índice do objetivo atual
        UpdateObjectivesText(); // Chama a função para atualizar o texto dos objetivos
    }

    //DELETAR ISSO DPS DA DEMO/ALPHA
    public void ShowDemoText()
    {
        _textPause.gameObject.SetActive(false); // Desativa o texto de pausa
        _textDemo1.gameObject.SetActive(true); // Ativa o texto de demonstração 1
        _textDemo2.gameObject.SetActive(true); // Ativa o texto de demonstração 2
        _textDemo1.text = "THANK YOU FOR PLAYING THE ALPHA OF THE BARKVENTURES!";
        _textDemo2.text = "GIVE US YOUR FEEDBACK ABOUT THE GAME AND HAVE FUN EXPLORING THE ENVIRONMENT!";
        _pc.TooglePause();
    }
    public bool CompareAct(int actIndex)
    {
        return CurrentObjective == actIndex; // Compara o índice do objetivo atual com o índice passado como parâmetro
    }
}
