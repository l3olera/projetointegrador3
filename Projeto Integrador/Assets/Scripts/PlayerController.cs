using System;
using NUnit.Framework;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variáveis responsáveis pela movimentação
    [Header("Configurações de movimentação")]

    [Tooltip("Velocidade do movimento para frente do player")]
    public float moveSpeedForward = 10f; //Velocidade do movimento para frente do player
    [Tooltip("Velocidade do movimento para o lado do player")]
    public float moveSpeedSide = 7f; //Velocidade do movimento para o lado do player
    [Tooltip("Multiplicador de velocidade ao correr")]
    public float runMultiplier = 1.5f; //Multiplicador de velocidade ao correr

    public bool isRunning; // Armazena se o jogador está correndo ou não


    //Variaveis responsáveis pelo pulo
    [Header("Configurações de pulo")]
    [Tooltip("Força do pulo")]
    public float jumpForce = 8f; // Força do pulo
    [Tooltip("Define o que é considerado 'chão'")]
    public LayerMask groundLayer; // Define o que é considerado "chão"
    [Tooltip("Objeto auxiliar para detectar o chão")]
    public Transform groundCheck; // Objeto auxiliar para detectar o chão

    private Rigidbody rb;
    private bool isGrounded; // Verifica se o jogador está no chão

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Obtém o rigbody do player
    }

    // Update is called once per frame
    void Update()
    {
        // Checa se está no chão
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        //Verifica se o personagem está correndo ou não para multiplicar a velocidade
        isRunning = Input.GetKey(KeyCode.LeftShift);
        float speedMultiplier = isRunning ? runMultiplier : 1f;

        // Obtém os inputs
        float inputForward = Input.GetAxis("Vertical"); //Recebe o input quando o usuário pressionar para ir pra frente ou pra trás (Retorna entre 0 a 1)
        float inputRight = Input.GetAxis("Horizontal"); //Recebe o input quando o usuário pressionar para ir pra direita ou pra esquerda (Retorna entre 0 a 1)

        // Aplica o movimento com o multiplicador de velocidade
        Vector3 moveDirection = new Vector3(inputRight * moveSpeedSide, rb.linearVelocity.y, inputForward * moveSpeedForward) * speedMultiplier;
        rb.linearVelocity = moveDirection;
        //transform.Translate(moveDirection * Time.deltaTime);

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
