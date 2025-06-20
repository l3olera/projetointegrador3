using System.Collections;
using UnityEngine;


public class Lever : LeverBase
{
    public GameObject lightCircle;     // Referência ao objeto da luz (pode ser um pequeno sphere ou emissive sprite)
    public Lever[] linkedLevers; // Referência a outras alavancas que podem ser afetadas por esta

    [SerializeField] private LeverColor _currentColor = LeverColor.Red; // Começa em vermelho
    private LeverColor _defaultColor;
    private bool _dafaultState;
    [SerializeField] private GameObject _leverController; // Referência ao puzzle (gerente)
    private Renderer _lightRenderer; // Para trocar cor via material
    private LeverPuzzle _leverPuzzle; // Referência ao script do puzzle

    protected override void Start()
    {
        base.Start();
        _leverPuzzle = _leverController.GetComponent<LeverPuzzle>(); // Referência ao script do puzzle
        _defaultColor = _currentColor;
        _dafaultState = state;

        if (lightCircle != null)
        {
            _lightRenderer = lightCircle.GetComponent<Renderer>();

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
    public void SetAnimationState(bool state)
    {
        _anim.SetBool("LeverToggle", state); // Atualiza a animação de acordo com o estado
        StartCoroutine(ChangeAnimation());
    }

    IEnumerator ChangeAnimation()
    {
        yield return new WaitForSeconds(durationAnimation);

        state = !state;
        CycleColor(); // Altera cor da luz
        _leverPuzzle.ReceiveSignal(gameObject, _currentColor);
        canChange = true;
    }

    void CycleColor()
    {
        _currentColor = (LeverColor)(((int)_currentColor + 1) % _leverPuzzle.lengthCode); // Red → Green → Blue → Purple → Yellow → Red...

        UpdateLightColor(ColorForLight(_currentColor));
    }

    Color ColorForLight(LeverColor lc)
    {
        if (_lightRenderer != null)
        {
            if (_leverPuzzle.lengthCode == 3)
            {
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
    }
}
