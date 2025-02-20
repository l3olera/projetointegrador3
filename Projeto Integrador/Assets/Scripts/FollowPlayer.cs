using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Referência ao jogador
    public Vector3 offset = new Vector3(0.02f, 1.23f, -1.6f); // Offset da câmera
    public float smoothSpeed = 0.125f; // Suavização do movimento

    void LateUpdate()
    {
        // Calcula a posição desejada da câmera
        Vector3 desiredPosition = player.position + player.TransformDirection(offset);

        // Suaviza o movimento da câmera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Faz a câmera olhar para o jogador
        transform.LookAt(player);
    }
}