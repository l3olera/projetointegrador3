using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SmellTrail : MonoBehaviour
{
    public static SmellTrail Instance { get; private set; } // Instância singleton da classe SmellTrail
    public NavMeshAgent player; // Referência ao agente NavMesh (jogador)
    public Transform target; // Referência ao alvo para onde o caminho será traçado
    public bool canDrawPath = false; // Controla se o caminho deve ser desenhado ou não
    [SerializeField] private LineRenderer _lineRenderer; // Componente responsável por desenhar o caminho
    [SerializeField] private float _pathHeightOffset; // Offset de altura para o caminho (evita sobreposição com o chão)

    private Coroutine drawPathCoroutine; // Referência à corrotina que desenha o caminho

    void Awake()
    {
        if (Instance != null && Instance != this) // Verifica se já existe uma instância
        {
            Destroy(this.gameObject); // Destroi o objeto atual se já houver uma instância
            return; // Sai do método para evitar duplicação
        }
        Instance = this; // Define a instância atual como a única instância
    }

    void Start()
    {
        _lineRenderer.gameObject.SetActive(false); // Desativa o LineRenderer no início
    }

    // Método chamado para ativar/desativar o desenho do caminho
    public void GenerateTrail()
    {
        if (drawPathCoroutine != null)
        {
            StopCoroutine(drawPathCoroutine); // Para a corrotina anterior, se existir
        }

        if (canDrawPath)
        {
            _lineRenderer.gameObject.SetActive(true); // Ativa o LineRenderer
            drawPathCoroutine = StartCoroutine(DrawPath()); // Inicia a corrotina para desenhar o caminho
        }
        else
        {
            _lineRenderer.gameObject.SetActive(false); // Desativa o LineRenderer
        }
    }

    // Corrotina responsável por atualizar o caminho do LineRenderer em tempo real
    private IEnumerator DrawPath()
    {
        while (canDrawPath)
        {
            player.SetDestination(target.position); // Define o destino do agente para o alvo

            var corners = player.path.corners; // Pega os pontos do caminho calculado pelo NavMesh
            int count = corners.Length; // Quantidade de pontos no caminho
            _lineRenderer.positionCount = count; // Atualiza o número de pontos do LineRenderer

            _lineRenderer.SetPosition(0, player.gameObject.transform.position + Vector3.up * _pathHeightOffset); // Define o primeiro ponto (posição do jogador com offset)

            if (count < 2)
            {
                yield return null; // Espera o próximo frame se não houver caminho suficiente
                continue; // Evita o loop abaixo se não há caminho suficiente
            }

            // Define os demais pontos do caminho no LineRenderer
            for (int i = 1; i < count; i++)
            {
                Vector3 points = new(corners[i].x, corners[i].y + _pathHeightOffset, corners[i].z); // Aplica offset de altura
                _lineRenderer.SetPosition(i, points); // Atualiza a posição no LineRenderer
            }

            yield return null; // Espera o próximo frame antes de atualizar novamente
        }
    }
}