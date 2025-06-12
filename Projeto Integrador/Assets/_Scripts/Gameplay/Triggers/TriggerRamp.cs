using UnityEngine;

public class TriggerRamp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<NavAgentController>(out var navAgent))
            {
                if (this.gameObject.CompareTag("Ramp/Begin"))
                {
                    navAgent.DisableAgent(); // Desativa o NavMeshAgent do jogador
                }
                else if (this.gameObject.CompareTag("Ramp/End"))
                {
                    navAgent.EnableAgent(); // Ativa o NavMeshAgent do jogador
                }
            }
        }
    }
}
