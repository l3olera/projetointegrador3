using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Localization;

public enum OccurrencesDialogue
{
    None,
<<<<<<< HEAD
    LeverPuzzleSolved, // Diálogo quando o puzzle da alavanca é resolvido
    PersecutionStart, // Diálogo quando a perseguição começa
=======
    EndAct2,
    LeverPuzzleSolved, // Diálogo quando o puzzle da alavanca é resolvido
    PersecutionStart, // Diálogo quando a perseguição começa
    EndGame
>>>>>>> Development
}


public class DialogueControl : MonoBehaviour
{
    [Header("Components")] // Cria uma seção no Inspector para melhor organização
    public GameObject dialogueObj; // Referência ao GameObject da caixa de diálogo
    public TextMeshProUGUI speechText; // Referência ao campo de texto onde aparecerá o diálogo
    public TextMeshProUGUI actorNameText; // Referência ao campo de texto onde aparecerá o nome do personagem

    [Header("Settings")] // Seção para configurações no Inspector
    public static DialogueControl Instance { get; private set; } // Instância única do DialogueControl
    public float typingSpeed; // Velocidade com que as letras aparecem na tela
    public bool canInteract = true; // Controla se o jogador pode interagir com o NPC para evitar que ele fica floodando o botão
    public static event Action<OccurrencesDialogue> OnDialogueEnd; // Evento disparado quando o diálogo termina
    public OccurrencesDialogue currentOccurrenceDialogue; // Referência ao scriptable object que contém as ocorrências do diálogo

    private bool _isTyping; // Indica se o diálogo está sendo digitado
    private float _typingSpeed; // Velocidade de digitação do texto privada para ser possível alterar a velocidade de digitação durante o código
    private DialogueLine[] _dialogueLines; // Array de falas do NPC
    private String[] _speechTranslate; // Array para armazenar as falas traduzidas
    private int _indexSpeechTranslate; // Índice da fala traduzida atual
    private int _currentLineIndex; // Índice da linha atual do diálogo
    private AudioSource _playSoundAnimal; // Referência ao AudioSource que toca os sons dos animais
    [SerializeField] private PlayerMovement _playerMovement; // Referência ao script que controla o movimento do jogador
    [SerializeField] private CinemachineInputAxisController _cinemachineCameraAxis; // Referência à câmera cinemática

<<<<<<< HEAD
=======
    void Awake()
    {
        if (Instance != null && Instance != this) // Verifica se já existe uma instância do DialogueControl
        {
            Destroy(this.gameObject); // Destroi o objeto atual se já houver uma instância
            return; // Retorna para evitar duplicação
        }

        Instance = this; // Inicializa a instância única do DialogueControl
    }

>>>>>>> Development
    void Start()
    {
        _playSoundAnimal = GetComponent<AudioSource>(); // Obtém o AudioSource do GameObject atual
        ResetOccurrenceDialogue(); // Reseta a ocorrência do diálogo no início
    }

    public void DefineOccurrenceDialogue(OccurrencesDialogue occurrence)
    {
        currentOccurrenceDialogue = occurrence; // Define a ocorrência do diálogo atual
    }

    // Método responsável por exibir o diálogo na tela
    public void Speech(DialogueLine[] lines)
    {
        _typingSpeed = typingSpeed;
<<<<<<< HEAD
        canInteract = false; // Impede o jogador de interagir enquanto o diálogo está ativo
=======
>>>>>>> Development
        _playerMovement.StopMovement(); // Para o movimento do jogador
        _playerMovement.canMove = false; // Desativa a movimentação do jogador durante o diálogo
        canInteract = false; // Impede o jogador de interagir enquanto o diálogo está ativo
        _playerMovement.FreeMouse();
        DisableCameraControl(); // Desativa o controle da câmera para evitar movimentos indesejados
        dialogueObj.SetActive(true); // Ativa a caixa de diálogo na tela
        _dialogueLines = lines; // Define as falas do NPC
        _currentLineIndex = 0; // Reseta o índice da linha atual
        DisplayCurrentLine(); // Exibe a primeira linha do diálogo
    }

    void DisplayCurrentLine()
    {
        if (_currentLineIndex < _dialogueLines.Length)
        {
            DialogueLine currentLine = _dialogueLines[_currentLineIndex];
            StopAllCoroutines(); // Para todas as corrotinas em execução para evitar sobreposição de diálogos
            StartCoroutine(TranslateNameAndDisplay(currentLine.actor.animalName)); // Atualiza o nome do personagem
            _typingSpeed = typingSpeed; // Reseta a velocidade de digitação
            TranslateText(currentLine.speechText); // Chama o método para traduzir o texto
            PlaySoundAnimal(currentLine.actor); // Toca o som do animal correspondente
        }
        else
        {
            EndDialogue(); // Finaliza o diálogo quando todas as falas foram exibidas
        }
    }

    IEnumerator TranslateNameAndDisplay(LocalizedString actorNameLocalized)
    {
        // Aguarda a tradução do nome do personagem
        var handle = actorNameLocalized.GetLocalizedStringAsync();
        yield return handle;

        actorNameText.text = handle.Result; // Atualiza o texto com a tradução
    }

    void TranslateText(Array speechTextArray)
    {
        _speechTranslate = new string[speechTextArray.Length]; // Inicializa o array com o tamanho correto
        _indexSpeechTranslate = 0; // Reseta o índice da fala traduzida

        foreach (LocalizedString currentText in speechTextArray) // Converte a frase atual em um array de caracteres
        {
            currentText.GetLocalizedStringAsync().Completed += handle =>
            {
                _speechTranslate[_indexSpeechTranslate] = handle.Result; // Atualiza o texto na tela com a tradução
                _indexSpeechTranslate++; // Avança para a próxima fala traduzida
            };
        }
        StartCoroutine(WaitForTranslationsAndType()); // Inicia a corrotina para exibir as letras do diálogo
    }

    void PlaySoundAnimal(Animal animal)
    {
        animal.PlaySound(_playSoundAnimal); // Toca o som do animal usando o AudioSource
    }

    IEnumerator WaitForTranslationsAndType()
    {
        while (_indexSpeechTranslate < _speechTranslate.Length)
        {
            yield return null; // Aguarda até que todas as traduções sejam carregadas
        }

        StartCoroutine(TypeSentence(_speechTranslate)); // Inicia a exibição do texto
    }

    // Corrotina para exibir as letras do diálogo uma por uma, simulando digitação
    IEnumerator TypeSentence(Array speechTextArray)
    {
        _isTyping = true; // Indica que o diálogo está sendo digitado
        speechText.text = ""; // Limpa o texto antes de exibir a nova fala

        foreach (string currentText in speechTextArray) // Converte a frase atual em um array de caracteres
        {
            foreach (char letter in currentText.ToCharArray()) // Converte a frase atual em um array de caracteres
            {
                speechText.text += letter; // Adiciona cada letra ao texto na tela
                yield return new WaitForSeconds(_typingSpeed); // Aguarda um pequeno intervalo entre as letras

                if (!_isTyping)
                {
                    _typingSpeed = 0f;
                }
            }
        }

        _isTyping = false; // Indica que o diálogo não está mais sendo digitado
    }

    // Método chamado para avançar para a próxima frase do diálogo, acelerando o processo
    public void NextSentence()
<<<<<<< HEAD
    {
        if (_isTyping)
        {
            _isTyping = false; // Se o diálogo ainda estiver sendo digitado, interrompe a digitação
            return; // Retorna para evitar que a próxima linha seja exibida
        }

        _currentLineIndex++; // Avança para a próxima linha
        DisplayCurrentLine(); // Exibe a próxima linha
    }

    void EndDialogue()
    {
        OnDialogueEnd?.Invoke(currentOccurrenceDialogue); // Dispara o evento de fim de diálogo
        ResetOccurrenceDialogue(); // Reseta a ocorrência do diálogo
        dialogueObj.SetActive(false); // Esconde a caixa de diálogo
        canInteract = true; // Permite que o jogador interaja novamente
        _playerMovement.canMove = true; // Reativa a movimentação do jogador
        _playerMovement.LockMouse();
        EnableCameraControl(); // Restaura o controle da câmera
    }

    void DisableCameraControl()
    {
        _cinemachineCameraIn.enabled = false; // Define uma prioridade baixa para desativar a câmera
    }

    void EnableCameraControl()
    {
        _cinemachineCameraIn.enabled = true; // Define uma prioridade alta para reativar a câmera
    }

    void ResetOccurrenceDialogue()
=======
>>>>>>> Development
    {
        currentOccurrenceDialogue = OccurrencesDialogue.None; // Reseta a ocorrência do diálogo
    }

    void EndDialogue()
    {
        OnDialogueEnd?.Invoke(currentOccurrenceDialogue); // Dispara o evento de fim de diálogo
        ResetOccurrenceDialogue(); // Reseta a ocorrência do diálogo
        dialogueObj.SetActive(false); // Esconde a caixa de diálogo
        canInteract = true; // Permite que o jogador interaja novamente
        _playerMovement.canMove = true; // Reativa a movimentação do jogador
        _playerMovement.LockMouse();
        EnableCameraControl(); // Restaura o controle da câmera
    }

    void DisableCameraControl()
    {
        _cinemachineCameraAxis.enabled = false; // Desativa a câmera
    }

    void EnableCameraControl()
    {
        _cinemachineCameraAxis.enabled = true; // Ativa a câmera
    }

    void ResetOccurrenceDialogue()
    {
        currentOccurrenceDialogue = OccurrencesDialogue.None; // Reseta a ocorrência do diálogo
    }
}
