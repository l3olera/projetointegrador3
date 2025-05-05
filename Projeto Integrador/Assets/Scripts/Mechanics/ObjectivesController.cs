using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Utilities; // Importa a biblioteca TextMeshPro para manipulação de texto

public class ObjectivesController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _objectivesText; // Referência ao texto de objetivos
    [SerializeField] private LocalizedString[] _objectivesTranslate; // Referência ao texto traduzido dos objetivos

    public int CurrentObjective {get; private set;} // variável privada que armazena o índice do objetivo atual

    void Start()
    {
        CurrentObjective = 1; // Inicializa o índice do objetivo atual como 1
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

    public void IncreaseActIndex(){
        CurrentObjective++; // Incrementa o índice do objetivo atual
        UpdateObjectivesText(); // Chama a função para atualizar o texto dos objetivos
    }
}
