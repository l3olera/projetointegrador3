<<<<<<< HEAD
=======
using System.Collections;
>>>>>>> Development
using UnityEngine;

public class TriggerRamp : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.gameObject.CompareTag("Ramp/Begin"))
            {
                OnRampBegin(other); // Chama o método quando o jogador começar a subir a rampa
            }
            else if (this.gameObject.CompareTag("Ramp/End"))
            {
                OnRampEnd(other); // Chama o método quando o jogador chegar ao final da rampa
            }

        }
    }

    void OnRampBegin(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
        {
<<<<<<< HEAD
=======
            playerMovement.StopMovement();
>>>>>>> Development
            playerMovement.canMove = false; // Desativa a movimentação do jogador
        }

        if (other.TryGetComponent<NavAgentController>(out var navAgent))
        {
<<<<<<< HEAD

            navAgent.DisableAgent(); // Desativa o NavMeshAgent do jogador
=======
            StartCoroutine(IntervalToDsableNavAgent(navAgent));
>>>>>>> Development
        }

        playerMovement.canMove = true; // Reativa a movimentação do jogador
    }

<<<<<<< HEAD
=======
    IEnumerator IntervalToDsableNavAgent(NavAgentController navAgent)
    {
        yield return null;
        navAgent.DisableAgent(); // Desativa o NavMeshAgent do jogador
    }

>>>>>>> Development
    void OnRampEnd(Collider other)
    {
        if (other.TryGetComponent<NavAgentController>(out var navAgent))
        {
            navAgent.EnableAgent(); // Ativa o NavMeshAgent do jogador
        }
    }
}