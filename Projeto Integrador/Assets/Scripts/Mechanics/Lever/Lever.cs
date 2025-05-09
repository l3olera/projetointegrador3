using System.Collections;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public float durationAnimation = 1f;
    public bool canChange = true;

    public GameObject lightCircle;     // Referência ao objeto da luz (pode ser um pequeno sphere ou emissive sprite)

    private bool state = false; // Estado da alavanca (abaixada ou levantada)
    private LeverColor currentColor = LeverColor.Red; // Começa em vermelho
    private bool playerInRange = false; // Verifica se o jogador está na área de interação

    [SerializeField] private GameObject leverController; // Referência ao puzzle (gerente)
    private Renderer lightRenderer; // Para trocar cor via material
    private Animator _anim; // Para animação de abaixar e levantar

    void Start() {
        _anim = GetComponent<Animator>(); // Referência ao Animator da alavanca

        if(lightCircle != null) {
            lightRenderer = lightCircle.GetComponent<Renderer>();
            UpdateLightColor(); // Aplica a cor inicial
        }
    }

    void Update() {
        if(leverController.GetComponent<LeverPuzzle>().endPuzzle && canChange) {
            canChange = false; //Impede o jogador de mudar a alavanca enquanto o puzzle não for resolvido
        } 

        if(canChange && playerInRange &&  (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E)))
        {
            canChange = false;

            if(!state){
                _anim.SetBool("LeverToggle", true); // Alavanca levantada
            } else {
                _anim.SetBool("LeverToggle", false); // Alavanca levantada
            }

            StartCoroutine(changeAnimation());
        }
    }
    
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            playerInRange = true; // Jogador entrou na área de interação
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            playerInRange = false; // Jogador saiu da área de interação
        }
    }

    IEnumerator changeAnimation(){
        yield return new WaitForSeconds(durationAnimation);

        state = !state;
        CycleColor(); // Altera cor da luz
        leverController.GetComponent<LeverPuzzle>().receiveSignal(gameObject, currentColor);

        canChange = true;
    }

    void CycleColor() {
        currentColor = (LeverColor)(((int)currentColor + 1) % 3); // Red → Green → Blue → Red...
        UpdateLightColor();
    }

    void UpdateLightColor() {
        if(lightRenderer != null){
            switch(currentColor){
                case LeverColor.Red:   lightRenderer.material.color = Color.red; break;
                case LeverColor.Green: lightRenderer.material.color = Color.green; break;
                case LeverColor.Blue:  lightRenderer.material.color = Color.blue; break;
            }
        }
    }
}
