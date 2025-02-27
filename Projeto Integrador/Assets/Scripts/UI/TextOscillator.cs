using UnityEngine;

public class TextOscillator : MonoBehaviour
{
    [Header("Referências de objetos")]
    [Tooltip("Componente RectTransform do texto que você quer oscilar o movimento")]
    public RectTransform textTransform;  // Referência ao RectTransform do texto

    [Header("Atributos de oscilação")]
    [Tooltip("Define a amplitude no eixo X da oscilacao")]
    public float amplitudeX = 10f;        // Quanto o texto vai se mover para o X

    [Tooltip("Define a amplitude no eixo Y da oscilacao")]
    public float amplitudeY = 1.5f;        // Quanto o texto vai se mover para o Y

    [Tooltip("Define a frequência da oscilacao")]
    public float frequency = 1f;         // Velocidade da oscilação

    private Vector3 initialPosition;

    void Start()
    {
        // Guarda a posição inicial do texto
        initialPosition = textTransform.anchoredPosition;
    }

    void Update()
    {
        // Atualiza a posição X do texto de acordo com o seno do tempo
        float newX = initialPosition.x + Mathf.Sin(Time.time * frequency) * amplitudeX;
        float newY = initialPosition.y + Mathf.Sin(Time.time * frequency) * amplitudeY;
        
        // Aplica a nova posição ao texto
        textTransform.anchoredPosition = new Vector3(newX, newY, 0f);
    }
}
