using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    public GameObject[] levers; //Armazena todos os gameObjects de alavancas

    public bool[] correctCode; //Armazena o código correto
    private bool[] actualCode; //Armazena o código que o jogador está colocando
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actualCode = new bool[correctCode.Length]; //Coloca o mesmo tamanho dos códigos corretos no codigo atual
    }

    //Coloque aqui o que vai acontecer se o código estiver correto
    void IfCorrectCode(){
        
    }

    //Recebe o sinal quando trocarem o estado de uma alavanca
    public void receiveSignal(GameObject go, bool state){
        //Coloca o estado trocado na posição exata
        for(int i =0;i < levers.Length; i++){
            if(go == levers[i]){
                actualCode[i] = state; 
                break;
            }
        }
    }

    //Função que verifica se o código das alavancas está certo ou não
    void check(){
        bool answer = true; //Resposta que armazena se está certo ou não, true para sim e false para não

        for(int i = 0; i < correctCode.Length; i++){
            if(actualCode[i] != correctCode[i]){
                answer = false;
                break;
            }
        }

        //Impede o jogador de interagir com as alavancas depois de estiverem corretas
        if(answer){
            foreach (GameObject l in levers)
            {
                l.GetComponent<Lever>().canChange = false;
            }   
        }
    }

   
}
