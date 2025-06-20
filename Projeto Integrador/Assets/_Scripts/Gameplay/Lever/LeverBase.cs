using System.Collections;
using UnityEngine;

public abstract class LeverBase : MonoBehaviour
{
    protected float durationAnimation = 1f;
    protected TextInteractManager _textInteractManager; // Para gerenciar texto de interação
    protected InputManager _im;
    protected Animator _anim; // Para animação de abaixar e levantar
    [SerializeField] protected string _translateName; // Nome da tradução para o texto de interação
    [SerializeField] protected LeverPuzzle _leverPuzzle; // Referência ao puzzle (gerente)
    private bool _playerInRange = false; // Verifica se o jogador está na área de interação
    public bool canChange = true;
    public bool state = false; // Estado da alavanca (abaixada ou levantada)

    protected virtual void Start()
    {
        _textInteractManager = TextInteractManager.Instance; // Obtém a referência ao gerenciador de texto de interação
        _im = InputManager.Instance;
        _anim = GetComponent<Animator>(); // Referência ao Animator da alavanca
    }

    protected virtual void Update()
    {
        if (canChange && _playerInRange && !_leverPuzzle.endPuzzle)
        {
            _textInteractManager.SetText(LocalizationManager.Instance.GetTranslation(_translateName)); // Define o texto de interação

            if (_im.IsInteractKeyPressed())
            {
                canChange = false;

                LeverEvent();
            }
        }
    }

    public void EnableInteract()
    {
        _playerInRange = true; // Jogador entrou na área de interação
        _textInteractManager.canSetText = true; // Permite que o texto de interação seja definido
    }

    public void DisableInteract()
    {
        _textInteractManager.canSetText = false; // Impede que o texto de interação seja definido
        _playerInRange = false; // Jogador saiu da área de interação
    }

    protected virtual void SetOwnAnimation()
    {
        _anim.SetBool("LeverToggle", state); // Define a animação de acordo com o estado atual
        state = !state; // Inverte o estado da alavanca
        canChange = true;
    }

    protected abstract void LeverEvent();
}