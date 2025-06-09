using System.Collections;
using UnityEngine;


public class Lever : MonoBehaviour
{
    public float durationAnimation = 1f;
    public bool canChange = true;

    public GameObject lightCircle;     // Referência ao objeto da luz (pode ser um pequeno sphere ou emissive sprite)
    public Lever[] linkedLevers; // Referência a outras alavancas que podem ser afetadas por esta

    private bool _state = false; // Estado da alavanca (abaixada ou levantada)
    [SerializeField] private LeverColor _currentColor = LeverColor.Red; // Começa em vermelho
    private bool _playerInRange = false; // Verifica se o jogador está na área de interação

    [SerializeField] private GameObject _leverController; // Referência ao puzzle (gerente)
    [SerializeField] private string _translateName; // Nome da tradução para o texto de interação
    private Renderer _lightRenderer; // Para trocar cor via material
    private Animator _anim; // Para animação de abaixar e levantar
    private TextInteractManager _textInteractManager; // Para gerenciar texto de interação
    private LeverPuzzle _leverPuzzle; // Referência ao script do puzzle

    void Start()
    {
        _anim = GetComponent<Animator>(); // Referência ao Animator da alavanca
        _leverPuzzle = _leverController.GetComponent<LeverPuzzle>(); // Referência ao script do puzzle

        if (lightCircle != null)
        {
            _lightRenderer = lightCircle.GetComponent<Renderer>();
            UpdateLightColor(); // Aplica a cor inicial
        }
    }

    void Update()
    {
        if (_textInteractManager == null) // Verifica se o TextInteractManager não foi encontrado
        {
            _textInteractManager = ReferenceManager.Instance.textInteractManager; // Tenta encontrar novamente o TextInteractManager
        }

        if (_leverPuzzle.endPuzzle && canChange)
        {
            canChange = false; //Impede o jogador de mudar a alavanca enquanto o puzzle não for resolvido
        }

        if (canChange && _playerInRange)
        {
            _textInteractManager.SetText(LocalizationManager.Instance.GetTranslation(_translateName)); // Define o texto de interação

            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E))
            {
                canChange = false;
                SetOwnAnimation(); // Define a animação da própria alavanca
                foreach (Lever lever in linkedLevers)
                {
                    if (lever != null)
                    {
                        lever.SetAnimationState(!_state); // Atualiza o estado da animação das alavancas vinculadas
                    }
                }
            }
        }
    }

    public void SetAnimationState(bool state)
    {
        _anim.SetBool("LeverToggle", state); // Atualiza a animação de acordo com o estado
        StartCoroutine(ChangeAnimation());
    }

    void SetOwnAnimation()
    {
        _anim.SetBool("LeverToggle", !_state); // Define a animação de acordo com o estado atual
        canChange = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true; // Jogador entrou na área de interação
            _textInteractManager.canSetText = true; // Permite que o texto de interação seja definido
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _textInteractManager.canSetText = false; // Impede que o texto de interação seja definido
            _playerInRange = false; // Jogador saiu da área de interação
        }
    }

    IEnumerator ChangeAnimation()
    {
        yield return new WaitForSeconds(durationAnimation);

        _state = !_state;
        CycleColor(); // Altera cor da luz
        _leverPuzzle.ReceiveSignal(gameObject, _currentColor);

        canChange = true;
    }

    void CycleColor()
    {
        _currentColor = (LeverColor)(((int)_currentColor + 1) % _leverPuzzle.lengthCode); // Red → Green → Blue → Purple → Yellow → Red...
        UpdateLightColor();
    }

    void UpdateLightColor()
    {
        if (_lightRenderer != null)
        {
            switch (_currentColor)
            {
                case LeverColor.Red: _lightRenderer.material.color = Color.red; break;
                case LeverColor.Green: _lightRenderer.material.color = Color.green; break;
                case LeverColor.Blue: _lightRenderer.material.color = Color.blue; break;
                case LeverColor.Purple: _lightRenderer.material.color = new Color(0.5f, 0f, 0.5f); break; // Cor roxa
                case LeverColor.Yellow: _lightRenderer.material.color = Color.yellow; break;
            }
        }
    }
}
