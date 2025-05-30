using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Localization;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")] // Cria uma seção no Inspector para melhor organização
    public GameObject dialogueObj; // Referência ao GameObject da caixa de diálogo
    public TextMeshProUGUI speechText; // Referência ao campo de texto onde aparecerá o diálogo
    public TextMeshProUGUI actorNameText; // Referência ao campo de texto onde aparecerá o nome do personagem

    [Header("Settings")] // Seção para configurações no Inspector
    public float typingSpeed; // Velocidade com que as letras aparecem na tela
    public bool canInteract = true; // Controla se o jogador pode interagir com o NPC para evitar que ele fica floodando o botão

    private bool _isTyping; // Indica se o diálogo está sendo digitado
    private float _typingSpeed; // Velocidade de digitação do texto privada para ser possível alterar a velocidade de digitação durante o código
    private DialogueLine[] _dialogueLines; // Array de falas do NPC
    private String[] _speechTranslate; // Array para armazenar as falas traduzidas
    private int _indexSpeechTranslate; // Índice da fala traduzida atual
    private int _currentLineIndex; // Índice da linha atual do diálogo
    private AudioSource _playSoundAnimal; // Referência ao AudioSource que toca os sons dos animais
    private PlayerMovement _playerMovement; // Referência ao script que controla o movimento do jogador
    private CinemachineInputAxisController _cinemachineCameraIn; // Referência à câmera cinemática

    void Start()
    {
        ReferenceManager.Instance.dialogueControl = this; // Inicializa a referência ao DialogueControl no ReferenceManager 
        _playerMovement = ReferenceManager.Instance.playerMovement; // Inicializa a referência ao PlayerMovement no ReferenceManager
        _cinemachineCameraIn = ReferenceManager.Instance.cinemachineCameraIn; // Inicializa a referência à CinemachineCamera no ReferenceManager
        _playSoundAnimal = GetComponent<AudioSource>(); // Obtém o AudioSource do GameObject atual
    }

    // Método responsável por exibir o diálogo na tela
    public void Speech(DialogueLine[] lines)
    {
        _typingSpeed = typingSpeed;
        canInteract = false; // Impede o jogador de interagir enquanto o diálogo está ativo
        _playerMovement.canMove = false; // Desativa a movimentação do jogador durante o diálogo
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

    void PlaySoundAnimal(Animal animal)
    {
        animal.PlaySound(_playSoundAnimal); // Toca o som do animal usando o AudioSource
    }

    void EndDialogue()
    {
        dialogueObj.SetActive(false); // Esconde a caixa de diálogo
        canInteract = true; // Permite que o jogador interaja novamente
        _playerMovement.canMove = true; // Reativa a movimentação do jogador
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

    // Método chamado para avançar para a próxima frase do diálogo
    public void NextSentence()
    {
        if (_isTyping)
        {
            _isTyping = false; // Se o diálogo ainda estiver sendo digitado, interrompe a digitação
            return; // Retorna para evitar que a próxima linha seja exibida
        }

        _currentLineIndex++; // Avança para a próxima linha
        DisplayCurrentLine(); // Exibe a próxima linha
    }
}
