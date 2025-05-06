using Unity.Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    public bool isWalking; // Armazena se o jogador está andando ou não
    public bool isRunning; // Armazena se o jogador está correndo ou não
    public bool canMove = true; // Armazena se o jogador pode se mover ou não, útil para pausar o jogo ou desativar a movimentação em certas situações, como durante um diálogo

    private float speedMultiplier;

    private Rigidbody _rb;
    private Transform _cameraTransform; // Adicionando referência à câmera
    
    [Header("Referências")]
    [SerializeField] private CinemachineCamera _cinemachineCamera; //Referência a cinemachine
    [SerializeField] private Animator _anim; //Referência ao animator do personagem

    [Header("Configurações de FOV")]
    [SerializeField] private float _normalFOV; //Guarda o fov normal do cachorro andando
    [SerializeField] private float _speedFOV; //Guarda o fov de quando o cachorro corre
    [SerializeField] private float _transitionSpeed; //Velocidade de transição suave do fov
    private float _targetFOV; //Alvo de fov que será colocado a cada update


    void Start()
    {
        ReferenceManager.Instance.playerMovement = this; // Inicializa a referência ao PlayerMovement no ReferenceManager
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
        if(canMove){ // Verifica se o jogador pode se mover
            Movement(); // Chama a função de movimentação
            Run(); // Chama a função de correr
        }else{
            _anim.SetBool("isWalking", false); // Para a animação de andar se o jogador não puder se mover
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
        isWalking = moveDirection.magnitude > 0.1f;
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
        if (isRunning && isWalking){
            speedMultiplier = runMultiplier;
            _targetFOV = _speedFOV;
        }else{
            speedMultiplier = 1f;
            _targetFOV = _normalFOV;
        }
    }
}
