using UnityEngine;
using TMPro;
using UnityEngine.Localization;

public class ObjectivesController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _objectivesText; // Referência ao texto de objetivos
    [SerializeField] private LocalizedString[] _objectivesTranslate; // Referência ao texto traduzido dos objetivos

    public int CurrentObjective { get; private set; } // variável privada que armazena o índice do objetivo atual
    public static ObjectivesController Instance { get; private set; } // Instância única do ObjectivesController

    void Awake()
    {
        if (Instance != null && Instance != this) // Verifica se já existe uma instância do ObjectivesController
        {
            Destroy(this.gameObject); // Destroi o objeto atual se já houver uma instância
            return; // Retorna para evitar duplicação
        }
        Instance = this;
    }

    void Start()
    {
        CurrentObjective = 4; // Inicializa o índice do objetivo atual como 1
        UpdateObjectivesText(); // Chama a função para atualizar o texto dos objetivos ao iniciar o jogo   
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

    public bool CompareAct(int actIndex)
    {
        return CurrentObjective == actIndex; // Compara o índice do objetivo atual com o índice passado como parâmetro
    }
}
