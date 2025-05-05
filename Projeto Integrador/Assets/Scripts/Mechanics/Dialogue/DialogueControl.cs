using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")] // Cria uma seção no Inspector para melhor organização
    public GameObject dialogueObj; // Referência ao GameObject da caixa de diálogo
    public TextMeshProUGUI speechText; // Referência ao campo de texto onde aparecerá o diálogo
    public TextMeshProUGUI actorNameText; // Referência ao campo de texto onde aparecerá o nome do personagem

    [Header("Settings")] // Seção para configurações no Inspector
    public float typingSpeed; // Velocidade com que as letras aparecem na tela
    public bool canInteract = true; // Controla se o jogador pode interagir com o NPC para evitar que ele fica floodando o botão
    private string[] sentences; // Armazena as falas do NPC
    private int index; // Índice da frase atual no array de sentenças

    // Método responsável por exibir o diálogo na tela
    public void Speech(string[] txt, string actorName){
        canInteract = false; // Impede o jogador de interagir enquanto o diálogo está ativo
        dialogueObj.SetActive(true); // Ativa a caixa de diálogo na tela
        speechText.text = ""; // Garante que o texto será limpo antes de começar a digitação
        sentences = txt; // Define as falas do NPC
        actorNameText.text = actorName; // Define o nome do NPC na caixa de diálogo
        StartCoroutine(TypeSentence()); // Inicia a corrotina para exibir o texto gradualmente
    }

    // Corrotina para exibir as letras do diálogo uma por uma, simulando digitação
    IEnumerator TypeSentence(){
        foreach(char letter in sentences[index].ToCharArray()) // Converte a frase atual em um array de caracteres
        { 
            speechText.text += letter; // Adiciona cada letra ao texto na tela
            yield return new WaitForSeconds(typingSpeed); // Aguarda um pequeno intervalo entre as letras
        }
    }

    // Método chamado para avançar para a próxima frase do diálogo
    public void NextSentence()
    {
        if(speechText.text == sentences[index]) // Verifica se a frase foi completamente exibida
        { 
            if(index < sentences.Length - 1) // Se ainda houver frases restantes
            { 
                index++; // Passa para a próxima frase
                speechText.text = ""; // Limpa o texto antes de exibir a nova frase
                StartCoroutine(TypeSentence()); // Inicia a digitação da próxima frase
            }else // Se todas as frases foram exibidas
            { 
                speechText.text = ""; // Limpa o texto
                index = 0; // Reseta o índice do diálogo
                dialogueObj.SetActive(false); // Esconde a caixa de diálogo
                canInteract = true; // Permite que o jogador interaja novamente
            }
        }
    }
}
