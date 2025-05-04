using UnityEngine;

public class GateController : MonoBehaviour
{

    private Animator _animator; // Referência ao Animator do portão
    private bool _isOpen = false; // Verifica se o portão está aberto ou fechado

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>(); // Obtém o componente Animator do portão
    }

    public void OpenGate(){
        if (!_isOpen) // Verifica se o portão não está aberto
        {
            _animator.SetBool("isOpen", true); // Aciona o gatilho de abertura do portão no Animator
            _isOpen = true; // Define que o portão está aberto
        }
    }

    public void CloseGate(){
        if (_isOpen) // Verifica se o portão está aberto
        {
            _animator.SetBool("isOpen", false); // Aciona o gatilho de fechamento do portão no Animator
            _isOpen = false; // Define que o portão está fechado
        }
    }
}
