using System.Collections;
using UnityEngine;


<<<<<<< HEAD
public class Lever : MonoBehaviour
{
    public float durationAnimation = 1f;
    public bool canChange = true;

    public GameObject lightCircle;     // Referência ao objeto da luz (pode ser um pequeno sphere ou emissive sprite)
    public Lever[] linkedLevers; // Referência a outras alavancas que podem ser afetadas por esta

    public bool state = false; // Estado da alavanca (abaixada ou levantada)
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
=======
public class Lever : LeverBase
{
    public GameObject lightCircle;     // Referência ao objeto da luz (pode ser um pequeno sphere ou emissive sprite)
    public Lever[] linkedLevers; // Referência a outras alavancas que podem ser afetadas por esta

    [SerializeField] private LeverColor _currentColor = LeverColor.Red; // Começa em vermelho
    private LeverColor _defaultColor;
    private bool _dafaultState;
    private Renderer _lightRenderer; // Para trocar cor via material

    protected override void Start()
    {
        base.Start();
        _defaultColor = _currentColor;
        _dafaultState = state;
>>>>>>> Development

        if (lightCircle != null)
        {
            _lightRenderer = lightCircle.GetComponent<Renderer>();
<<<<<<< HEAD
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

                if (linkedLevers.Length > 1)
                {
                    SetOwnAnimation(); // Define a animação da própria alavanca
                }

                foreach (Lever lever in linkedLevers)
                {
                    if (lever != null)
                    {
                        lever.SetAnimationState(lever.state); // Atualiza o estado da animação das alavancas vinculadas
                    }
                }
            }
        }
    }

=======

            UpdateLightColor(ColorForLight(_currentColor)); // Aplica a cor inicial
        }
    }

    protected override void LeverEvent()
    {
        if (linkedLevers.Length > 1)
        {
            SetOwnAnimation(); // Define a animação da própria alavanca
        }

        foreach (Lever lever in linkedLevers)
        {
            if (lever != null)
            {
                lever.SetAnimationState(lever.state); // Atualiza o estado da animação das alavancas vinculadas
            }
        }
    }
>>>>>>> Development
    public void SetAnimationState(bool state)
    {
        _anim.SetBool("LeverToggle", state); // Atualiza a animação de acordo com o estado
        StartCoroutine(ChangeAnimation());
    }

<<<<<<< HEAD
    void SetOwnAnimation()
    {
        _anim.SetBool("LeverToggle", state); // Define a animação de acordo com o estado atual
        state = !state; // Inverte o estado da alavanca
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

=======
>>>>>>> Development
    IEnumerator ChangeAnimation()
    {
        yield return new WaitForSeconds(durationAnimation);

        state = !state;
        CycleColor(); // Altera cor da luz
        _leverPuzzle.ReceiveSignal(gameObject, _currentColor);
<<<<<<< HEAD

=======
>>>>>>> Development
        canChange = true;
    }

    void CycleColor()
    {
        _currentColor = (LeverColor)(((int)_currentColor + 1) % _leverPuzzle.lengthCode); // Red → Green → Blue → Purple → Yellow → Red...
<<<<<<< HEAD
        UpdateLightColor();
    }

    void UpdateLightColor()
=======

        UpdateLightColor(ColorForLight(_currentColor));
    }

    Color ColorForLight(LeverColor lc)
>>>>>>> Development
    {
        if (_lightRenderer != null)
        {
            if (_leverPuzzle.lengthCode == 3)
            {
<<<<<<< HEAD
                switch (_currentColor)
                {
                    case LeverColor.Red: _lightRenderer.material.color = Color.red; break;
                    case LeverColor.Green: _lightRenderer.material.color = Color.green; break;
                    case LeverColor.Blue: _lightRenderer.material.color = Color.blue; break;
                }
            }
            else
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
=======
                return lc switch
                {
                    LeverColor.Red => Color.red,
                    LeverColor.Green => Color.green,
                    LeverColor.Blue => Color.blue,
                    _ => Color.black,
                };
            }
            else
            {
                return lc switch
                {
                    LeverColor.Red => Color.red,
                    LeverColor.Green => Color.green,
                    LeverColor.Blue => Color.blue,
                    LeverColor.Purple => new Color(0.5f, 0f, 0.5f),// Cor roxa
                    LeverColor.Yellow => Color.yellow,
                    _ => Color.black,
                };
            }
        }
        return Color.black;
    }

    void UpdateLightColor(Color color)
    {
        _lightRenderer.material.color = color;
    }

    public void ResetToDefault()
    {
        canChange = true;

        _currentColor = _defaultColor;
        UpdateLightColor(ColorForLight(_defaultColor));

        state = !_dafaultState;
        SetOwnAnimation();
>>>>>>> Development
    }
}
