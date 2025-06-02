using UnityEngine;
using System.Collections.Generic;

public enum LeverColor
{
    Red,
    Green,
    Blue
}

public class LeverPuzzle : MonoBehaviour
{
    public bool endPuzzle = false; // Se o puzzle já foi resolvido
    public GameObject[] levers;
    public LeverColor[] correctCode; // Código correto, ex: [Blue, Red, Green]
    public GameObject[] CorrectsLightCircle; // Referência aos objetos de luz que mostram o código correto 

    private LeverColor[] _actualCode;
    [SerializeField] private GameObject _door; // Referência à porta que será aberta quando o puzzle for resolvido

    void Start()
    {
        _actualCode = new LeverColor[correctCode.Length];

        // Gera código aleatório para partida (opcional)
        GenerateRandomCode();
    }

    void GenerateRandomCode()
    {
        correctCode = new LeverColor[3];

        // Cria uma lista com todas as cores possíveis
        List<LeverColor> availableColors = new List<LeverColor> {
            LeverColor.Red,
            LeverColor.Green,
            LeverColor.Blue
        };

        // Embaralha a lista
        for (int i = 0; i < availableColors.Count; i++)
        {
            LeverColor temp = availableColors[i];
            int randomIndex = Random.Range(i, availableColors.Count);
            availableColors[i] = availableColors[randomIndex];
            availableColors[randomIndex] = temp;
        }

        // Pega as 3 primeiras cores da lista embaralhada
        for (int i = 0; i < correctCode.Length; i++)
        {
            correctCode[i] = availableColors[i];
        }

        // Exibir nas esferas do lado esquerdo
        for (int i = 0; i < CorrectsLightCircle.Length; i++)
        {
            if (CorrectsLightCircle[i] != null)
            {
                Renderer lightRenderer = CorrectsLightCircle[i].GetComponent<Renderer>();
                if (lightRenderer != null)
                {
                    lightRenderer.material.color = GetColorFromLeverColor(correctCode[i]);
                }
            }
        }
    }

    Color GetColorFromLeverColor(LeverColor color)
    {
        return color switch
        {
            LeverColor.Red => Color.red,
            LeverColor.Green => Color.green,
            LeverColor.Blue => Color.blue,
            _ => Color.white,// Cor padrão
        };
    }

    public void ReceiveSignal(GameObject go, LeverColor color)
    {
        for (int i = 0; i < levers.Length; i++)
        {
            if (go == levers[i])
            {
                _actualCode[i] = color;
                break;
            }
        }

        Check();
    }

    void Check()
    {
        bool correct = true;
        for (int i = 0; i < correctCode.Length; i++)
        {
            if (_actualCode[i] != correctCode[i])
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            endPuzzle = true; // Código correto, puzzle resolvido

            //Provavelmente ficará assim, irei destruir a barreira temporariamente
            _door.GetComponent<Animator>().SetBool("canOpen", true); // Abre a porta
        }
    }
}
