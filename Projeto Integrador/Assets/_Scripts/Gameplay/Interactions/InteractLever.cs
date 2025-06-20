using UnityEngine;
using UnityEngine.Events;

public class InteractLever : MonoBehaviour
{
    public UnityEvent OnPlayerEnterLeverZone; // Dispara um evento quando o jogador está na área de interação
    public UnityEvent OnPlayerExitLeverZone; // Dispara um evento quando o jogador sai da área de interação

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnterLeverZone?.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerExitLeverZone?.Invoke();
        }
    }
}