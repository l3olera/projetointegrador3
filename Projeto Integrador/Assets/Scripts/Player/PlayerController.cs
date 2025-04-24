using Unity.Cinemachine;
using UnityEngine;

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

    private Rigidbody _rb;
    private bool _isGrounded; // Verifica se o jogador está no chão
    private Transform _cameraTransform; // Adicionando referência à câmera
    [SerializeField] private CinemachineCamera _cinemachineCamera; //Referência a cinemachine
    [SerializeField] private Animator _anim; //Referência ao animator do personagem
    [SerializeField] private float _normalFOV; //Guarda o fov normal do cachorro andando
    [SerializeField] private float _speedFOV; //Guarda o fov de quando o cachorro corre
    [SerializeField] private float _transitionSpeed; //Velocidade de transição suave do fov
    private float _targetFOV; //Alvo de fov que será colocado a cada update

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>(); //Obtém o rigbody do player
        _cameraTransform = Camera.main.transform; // Obtém a câmera principal
        _targetFOV = _normalFOV;

        //Coloca o fov normal na camera
        if(_cinemachineCamera != null){
            _cinemachineCamera.Lens.FieldOfView = _normalFOV;
        }
    }

    void FixedUpdate()
    {
        Run(); // Chama a função de correr
        Movement(); // Chama a função de movimentação
        Jump(); // Chama a função de pulo
    }

    void Jump(){
        // Checa se está no chão
        _isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Movement(){
        // Obtém os inputs
        float inputForward = Input.GetAxis("Vertical"); // Input para frente/trás
        float inputRight = Input.GetAxis("Horizontal"); // Input para esquerda/direita

        // Cria um vetor de movimento baseado no input do jogador (espaço local)
        Vector3 moveDirection = new Vector3(inputRight, 0, inputForward).normalized;

        /*
        VER SE VAI MANTER ESSA TRANSFORMAÇÃO DE DIREÇÃO OU NÃO
        */
        // Converte o vetor para o espaço global com base na rotação da câmera
        moveDirection = _cameraTransform.TransformDirection(moveDirection); 
        moveDirection.y = 0; // Remove qualquer componente vertical


        // Aplica a movimentação no Rigidbody
        _rb.linearVelocity = new Vector3(moveDirection.x * moveSpeedSide * speedMultiplier, _rb.linearVelocity.y, moveDirection.z * moveSpeedForward * speedMultiplier);

        // Definir os parâmetros da animação
        bool isWalking = moveDirection.magnitude > 0.1f;
        _anim.SetBool("isWalking", isWalking);
        _anim.SetBool("isRunning", isWalking && isRunning);

        // Transição suave entre os valores de FOV
        if (_cinemachineCamera != null)
        {
            _cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(_cinemachineCamera.Lens.FieldOfView, _targetFOV, _transitionSpeed * Time.deltaTime);
        }

        // Apenas gira se houver uma direção válida de movimento
        if (isWalking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void Run(){
        // Verifica se o personagem está correndo ou não para multiplicar a velocidade e definir qual FOV usar
        isRunning = Input.GetKey(KeyCode.LeftShift);
        if (isRunning){
            speedMultiplier = runMultiplier;
            _targetFOV = _speedFOV;
        }else{
            speedMultiplier = 1f;
            _targetFOV = _normalFOV;
        }
    }
}


