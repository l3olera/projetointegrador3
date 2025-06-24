using UnityEngine;
using UnityEngine.Events;

public class InteractionRadius : MonoBehaviour
{
    public UnityEvent OnPlayerEnter;
    public UnityEvent OnPlayerExit;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnPlayerEnter?.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            OnPlayerExit?.Invoke();
    }
}