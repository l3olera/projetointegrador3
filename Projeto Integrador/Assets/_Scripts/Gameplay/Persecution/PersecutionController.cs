using System.Collections;
using TMPro;
using UnityEngine;

public class PersecutionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText; // Referência ao campo de texto que exibe o tempo restante
    [SerializeField] private float _persecutionTime = 90f; // Tempo total de perseguição em segundos
    [SerializeField] private PlayerSpawn _playerSpawn; // Referência ao PlayerSpawn para reiniciar o jogador
    [SerializeField] private UniqueDialogueTrigger _dialogueTriggerRuffus; // Referência ao UniqueDialogueTrigger para reativar o diálogo de perseguição, caso o jogador perca
    private InventoryController _ic; // Referência ao InventoryController
    private SmellTargetManager _smellTargetManager; // Referência ao SmellTargetManager
    private ObjectivesController _oc; // Referência ao ObjectivesController
    private bool _isInTrigger = false; // Indica se o jogador está dentro do gatilho de perseguição
    private OccurrencesDialogue _occurrenceDialogue;

    public float timer;
    public bool managedEscape = false; // Indica se o jogador conseguiu escapar
    public int idSpawn = 2; // ID do spawn para reiniciar o jogador

    void OnEnable()
    {
        DialogueControl.OnDialogueEnd += StartPersecution; // Inscreve-se no evento de fim de diálogo
    }

    void OnDisable()
    {
        DialogueControl.OnDialogueEnd -= StartPersecution; // Cancela a inscrição no evento de fim de diálogo
    }

    void Start()
    {
        _ic = InventoryController.Instance; // Obtém a referência ao InventoryController
        _oc = ObjectivesController.Instance; // Obtém a referência ao ObjectivesController
        _smellTargetManager = SmellTargetManager.Instance; // Obtém a referência ao SmellTargetManager

        _occurrenceDialogue = OccurrencesDialogue.PersecutionStart; // Define o ID da perseguição
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _ic.HasItemById(3)) // Verifica se o jogador entrou no gatilho de perseguição e possui o item com o pepito
        {
            _isInTrigger = true; // Marca que o jogador entrou no gatilho de perseguição
        }
    }

    void StartPersecution(OccurrencesDialogue dialogueID)
    {
        if (_isInTrigger && dialogueID == _occurrenceDialogue)
        {
            _isInTrigger = false; // Reseta o gatilho para evitar múltiplas ativações
            _oc.IncreaseActIndex(); // Incrementa o índice do objetivo atual
            _smellTargetManager.NextTarget(); // Avança para o próximo objetivo
            StartCoroutine(PersecutionTimer());
        }
    }

    IEnumerator PersecutionTimer()
    {
        timer = _persecutionTime;
        while (timer > 0)
        {
            if (managedEscape)
            {
                _timerText.text = "";
                _timerText.gameObject.SetActive(false); // Desativa o texto do temporizador se o jogador conseguiu escapar
                yield break; // Sai do loop se o jogador conseguiu escapar
            }

            _timerText.text = $"{timer:F1}";
            timer -= Time.deltaTime;
            yield return null;
        }

        _timerText.text = ""; // Limpa o texto do temporizador quando o tempo acabar
        DontEscape(); // Chama o método para limpar o texto quando o tempo acabar
    }

    void DontEscape()
    {
        if (!managedEscape)
        {
            _playerSpawn.SpawnPlayer(idSpawn); // Reinicia o jogador se ele não conseguiu escapar
            _dialogueTriggerRuffus.hasTriggered = false; // Reseta o gatilho do diálogo de perseguição
        }
    }
}
