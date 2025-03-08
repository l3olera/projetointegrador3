using System.Collections;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public float durationAnimation = 1f; //Número de segundos que demora para a animação acontecer
    private bool state = false; //Identifica se a alavanca está abaixada, false para não e true para sim
    public bool canChange = true; //Identifica se a animação pode mudar ou não, para evitar que o usuário interaja sem parar.

    public GameObject leverController;

    IEnumerator changeAnimation(){
        yield return new WaitForSeconds(durationAnimation); //Espera a animação acabar para, assim, permitir o jogador de interagir com as alavancas
        state = !state; //Inverte o valor

        leverController.GetComponent<LeverPuzzle>().receiveSignal(gameObject, state);
    }

    void Update()
    {
        //Impede que o jogador "spame" botão de interação e bugue a animação e consequentemente o código
        if(canChange){
            canChange = false;
            //Interage com a alavanca ao usar os botões Z e E.
            if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E)){
                if(!state){
                    GetComponent<Animation>().Play(); //Colocar nome da animação que desce a alavanca
                    StartCoroutine(changeAnimation());
                    
                }else {
                    GetComponent<Animation>().Play(); //Colocar nome da animação que levanta a alavanca
                    StartCoroutine(changeAnimation());
                }
            } 
        }
    }
}
