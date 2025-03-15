using System;
using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj; //Referencia o game Object que irá aparecer o dialogo
    public Image profile; //Referencia o objeto imagem que aparecera o sprite do personagem npc
    public TextMeshProUGUI speechText; //Referencia o objeto texto que vai aparecer o discurso
    public TextMeshProUGUI actorNameText; //Referencia o objeto texto que vai mostrar o nome do npc
    public bool canInteract = true;

    [Header("Settings")]
    public float typingSpeed; //Controla a velocidade que as palavras aparecerão
    private string[] sentences;
    private int index;

    //Responsável por mostrar o discurso de um determinado npc
    public void Speech(Sprite p, string[] txt, string actorName){
        canInteract = false;
        dialogueObj.SetActive(true); //Deixa amostra a caixa de dialago
        profile.sprite = p; //Coloca o sprite do npc
        sentences = txt; //Coloca o texto do discurso
        actorNameText.text = actorName; //Coloca o nome do npc
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence(){
        foreach(char letter in sentences[index].ToCharArray()){
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence(){
        if(speechText.text == sentences[index]){
            //ainda há textos
            if(index < sentences.Length - 1){
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }else{ //lido quando acaba os textos
                canInteract = true;
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
            }
        }
    }
}
