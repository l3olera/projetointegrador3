/*
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private Vector3 offset = new(0.02f, 1.23f, -1.6f); //== new Vector3(...) / Posição relativa à rotação do jogador

    // LateUpdate vem dps do Update
    void LateUpdate()
    {        
        // Atualiza a posição e rotação da câmera para seguir o jogador
        transform.SetPositionAndRotation(player.transform.position + offset, new Vector3(0, transform.position.y, 0f);
    }
}
*/

using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player; // Referência ao jogador
    [SerializeField] private Vector3 offset = new(0.02f, 1.23f, -1.6f); // Offset da câmera em relação ao jogador
    [SerializeField] private float smoothSpeed = 0.125f; // Suavização do movimento da câmera
    [SerializeField] private float rotationSpeed = 5f; // Velocidade de rotação da câmera

    private void LateUpdate()
    {
        // Verifica se o jogador foi atribuído
        if (player == null)
        {
            Debug.LogWarning("Jogador não atribuído à câmera.");
            return;
        }

        // Calcula a posição desejada da câmera com base no offset
        Vector3 desiredPosition = player.transform.position + player.transform.TransformDirection(offset);

        // Suaviza o movimento da câmera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Obtém a rotação Y do jogador
        float playerYRotation = player.transform.eulerAngles.y;

        // Define a rotação da câmera para seguir apenas o eixo Y do jogador
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, playerYRotation, transform.eulerAngles.z);

        // Suaviza a rotação da câmera
        Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = smoothedRotation;
    }
}