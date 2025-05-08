using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string[] speechText; // Array contendo as falas do NPC
    public string actorName; // Nome do NPC que será exibido na caixa de diálogo

    public LayerMask playerLayer; // Define a camada do jogador para detectar proximidade
    public float radious; // Raio da detecção de proximidade do NPC

    private DialogueControl _dc; // Referência ao script que controla os diálogos
    private bool _onRadious; // Indica se o jogador está dentro do raio de interação
    
    void FixedUpdate()
    {
        Interact(); // Chama a verificação de interação com o NPC a cada atualização da física do jogo
    }

    void Update()
    {
        if(_dc == null) // Verifica se o DialogueControl não foi encontrado
        {
            _dc = ReferenceManager.Instance.dialogueControl; // Tenta encontrar novamente o DialogueControl
        }
        // Se o jogador pressionar "E" ou "Z", estiver no raio de interação e puder interagir
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z)) && _onRadious && _dc.canInteract)
        {
            _dc.Speech(speechText, actorName); // Chama o diálogo do NPC
        }
    }

    // Método que verifica se o jogador está dentro do raio de interação
    public void Interact()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radious, playerLayer); // Cria uma esfera invisível que detecta colisões dentro do raio definido
        _onRadious = false; // Assume que o jogador não está perto

        foreach (Collider hit in hits) // Percorre todos os objetos detectados na esfera
        {
            if (hit.CompareTag("Player")) // Verifica se o objeto detectado é o jogador
            {
                _onRadious = true; // Marca que o jogador está dentro do raio
                break; // Sai do loop assim que encontrar o jogador, evitando verificações desnecessárias
            }
        }
    }

    // Método que desenha uma esfera no editor para visualizar a área de detecção do NPC    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radious); // Desenha a esfera com o raio definido no editor
    }
}
