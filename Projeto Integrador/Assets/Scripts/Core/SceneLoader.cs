using System.Collections;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Atributos")]
    [Tooltip("Armazena a duração que a transição terá em segundos")]
    [SerializeField] private float transitionDuration; //Armazena a duração que a transição terá em segundo
    
    [Header("Referências de classes e objetos")]
    [Tooltip("Referência do componente Animator da transição")]
    [SerializeField] private Animator transitionAnim; //Referência ao componente animator da transição

    public void Transition(string sceneName)
    {
        //Responsável por iniciar uma corrotina
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        //Seta o trigger para iniciar a animação do fade_start (Fade_out da fase para ai sim dar load em outra)
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionDuration); //Espera o tempo de duração da transição

        SceneManager.LoadScene(sceneName); //Direciona o jogador para a próxima cena
    }
}
