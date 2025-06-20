using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Referências de classes e objetos")]
    [Tooltip("Referência do componente Animator da transição")]
    [SerializeField] private FadeController _fc; //Referência ao componente animator da transição


    public void Transition(string sceneName)
    {
        //Responsável por iniciar uma corrotina
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        //Seta o trigger para iniciar a animação do fade_start (Fade_out da fase para ai sim dar load em outra)
        _fc.StartFade();

        yield return new WaitForSeconds(_fc.transitionDuration); //Espera o tempo de duração da transição

        SceneManager.LoadScene(sceneName); //Direciona o jogador para a próxima cena
    }
}
