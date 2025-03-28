using System;
using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;
//COMENTARIO NOVO
public class PlayerController : MonoBehaviour
{
    //Variáveis responsáveis pela movimentação
    [Header("Configurações de movimentação")]

    [Tooltip("Velocidade do movimento para frente do player")]
    public float moveSpeedForward; //Velocidade do movimento para frente do player
    [Tooltip("Velocidade do movimento para o lado do player")]
    public float moveSpeedSide; //Velocidade do movimento para o lado do player
    [Tooltip("Multiplicador de velocidade ao correr")]
    public float runMultiplier; //Multiplicador de velocidade ao correr
     [Tooltip("Velocidade de rotação")]
    public float rotationSpeed; //Velocidade de rotação

    public bool isRunning; // Armazena se o jogador está correndo ou não
    private float speedMultiplier;


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
    private Transform cameraTransform; // Adicionando referência à câmera
    [SerializeField] private CinemachineCamera cinemachineCamera; //Referência a cinemachine
    [SerializeField] private Animator anim; //Referência ao animator do personagem
    [SerializeField] private float normalFOV; //Guarda o fov normal do cachorro andando
    [SerializeField] private float speedFOV; //Guarda o fov de quando o cachorro corre
    [SerializeField] private float transitionSpeed; //Velocidade de transição suave do fov
    private float targetFOV; //Alvo de fov que será colocado a cada update

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Obtém o rigbody do player
        cameraTransform = Camera.main.transform; // Obtém a câmera principal
        targetFOV = normalFOV;

        //Coloca o fov normal na camera
        if(cinemachineCamera != null){
            cinemachineCamera.Lens.FieldOfView = normalFOV;
        }
    }

    
    void Update()
    {
        // Checa se está no chão
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        // Verifica se o personagem está correndo ou não para multiplicar a velocidade e definir qual FOV usar
        isRunning = Input.GetKey(KeyCode.LeftShift);
        if (isRunning){
            speedMultiplier = runMultiplier;
            targetFOV = speedFOV;
        }else{
            speedMultiplier = 1f;
            targetFOV = normalFOV;
        }

        // Obtém os inputs
        float inputForward = Input.GetAxis("Vertical"); // Input para frente/trás
        float inputRight = Input.GetAxis("Horizontal"); // Input para esquerda/direita

        // Cria um vetor de movimento baseado no input do jogador (espaço local)
        Vector3 moveDirection = new Vector3(inputRight, 0, inputForward).normalized;

        /*
        VER SE VAI MANTER ESSA TRANSFORMAÇÃO DE DIREÇÃO OU NÃO
        */
        // Converte o vetor para o espaço global com base na rotação da câmera
        moveDirection = cameraTransform.TransformDirection(moveDirection); 
        moveDirection.y = 0; // Remove qualquer componente vertical


        // Aplica a movimentação no Rigidbody
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeedSide * speedMultiplier, rb.linearVelocity.y, moveDirection.z * moveSpeedForward * speedMultiplier);

        // Definir os parâmetros da animação
        bool isWalking = moveDirection.magnitude > 0.1f;
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isRunning", isWalking && isRunning);

        // Transição suave entre os valores de FOV
        if (cinemachineCamera != null)
        {
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(cinemachineCamera.Lens.FieldOfView, targetFOV, transitionSpeed * Time.deltaTime);
        }

        // Apenas gira se houver uma direção válida de movimento
        if (isWalking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
