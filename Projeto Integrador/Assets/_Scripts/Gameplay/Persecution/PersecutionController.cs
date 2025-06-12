using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PersecutionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText; // Referência ao campo de texto que exibe o tempo restante
    [SerializeField] private float _persecutionTime = 90f; // Tempo total de perseguição em segundos
    [SerializeField] private PlayerSpawn _playerSpawn; // Referência ao PlayerSpawn para reiniciar o jogador
    private InventoryController _ic; // Referência ao InventoryController
    private SmellTargetManager _smellTargetManager; // Referência ao SmellTargetManager
    private bool _isInTrigger = false; // Indica se o jogador está dentro do gatilho de perseguição

    public float timer;
    public bool managedEscape = false; // Indica se o jogador conseguiu escapar

    void OnEnable()
    {
        DialogueControl.OnDialogueEnd += StartPersecution; // Inscreve-se no evento de fim de diálogo
    }

    void OnDisable()
    {
        DialogueControl.OnDialogueEnd -= StartPersecution; // Cancela a inscrição no evento de fim de diálogo
    }

    void Update()
    {
        if (_ic == null)
        {
            _ic = ReferenceManager.Instance.inventoryController; // Obtém a referência ao InventoryController do ReferenceManager
        }

        if (_smellTargetManager == null)
        {
            _smellTargetManager = ReferenceManager.Instance.smellTargetManager; // Obtém a referência ao SmellTargetManager do ReferenceManager
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _ic.HasItemById(3)) // Verifica se o jogador entrou no gatilho de perseguição e possui o item com o pepito
        {
            _isInTrigger = true; // Marca que o jogador entrou no gatilho de perseguição
        }
    }

    void StartPersecution()
    {
        if (_isInTrigger)
        {
            _isInTrigger = false; // Reseta o gatilho para evitar múltiplas ativações
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
            _playerSpawn.SpawnPlayer(1); // Reinicia o jogador se ele não conseguiu escapar
        }
    }
}
