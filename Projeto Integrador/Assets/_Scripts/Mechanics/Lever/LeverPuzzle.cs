using UnityEngine;
using System.Collections.Generic;

public enum LeverColor
{
    Red,
    Green,
    Blue,
    Purple,
    Yellow
}

public class LeverPuzzle : MonoBehaviour
{
    public bool endPuzzle = false; // Se o puzzle já foi resolvido
    public GameObject[] levers;
    public GameObject[] CorrectsLightCircle; // Referência aos objetos de luz que mostram o código correto 
    public int lengthCode; // Tamanho do código

    private LeverColor[] _actualCode;
    [SerializeField] private LeverColor[] correctCode; // Código correto, ex: [Blue, Red, Green]
    [SerializeField] private GameObject _door; // Referência à porta que será aberta quando o puzzle for resolvido
    [SerializeField] private LeverEffect[] _leverEffectMap;
    [SerializeField] private bool _canRandom = true; // Se o código pode ser gerado aleatoriamente

    void Start()
    {
        _actualCode = new LeverColor[lengthCode];

        // Gera código aleatório para partida
        if (_canRandom)
            GenerateRandomCode();
        else
            SetCorrectCode(); // Se não for aleatório, define o código correto manualmente

        SetLink(); // Configura as alavancas e seus efeitos
    }

    void SetLink()
    {
        for (int i = 0; i < levers.Length; i++)
        {
            if (levers[i].TryGetComponent<Lever>(out var lever))
            {
                Lever[] valueLinked = null;

                if (_leverEffectMap == null || _leverEffectMap.Length == 0)
                {
                    valueLinked = new Lever[1] { lever };
                }
                else
                {
                    valueLinked = _leverEffectMap[i].leverIndex == i ? _leverEffectMap[i].linkedLevers : new Lever[1] { lever };
                }

                lever.linkedLevers = valueLinked; // Define as alavancas vinculadas
            }
        }
    }

    void SetCorrectCode()
    {
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
    void GenerateRandomCode()
    {
        correctCode = new LeverColor[lengthCode];
        List<LeverColor> availableColors;

        if (lengthCode == 3)
        {
            availableColors = new()
            {
                LeverColor.Red,
                LeverColor.Green,
                LeverColor.Blue,
            };
        }
        // Cria uma lista com todas as cores possíveis
        else
        {
            availableColors = new()
            {
                LeverColor.Red,
                LeverColor.Green,
                LeverColor.Blue,
                LeverColor.Purple,
                LeverColor.Yellow
            };
        }

        // Embaralha a lista
        for (int i = 0; i < availableColors.Count; i++)
        {
            LeverColor temp = availableColors[i];
            int randomIndex = Random.Range(i, availableColors.Count);
            availableColors[i] = availableColors[randomIndex];
            availableColors[randomIndex] = temp;
        }

        // Pega as 5 primeiras cores da lista embaralhada
        for (int i = 0; i < correctCode.Length; i++)
        {
            correctCode[i] = availableColors[i];
        }

        SetCorrectCode(); // Atualiza as esferas com o código correto
    }

    Color GetColorFromLeverColor(LeverColor color)
    {
        return color switch
        {
            LeverColor.Red => Color.red,
            LeverColor.Green => Color.green,
            LeverColor.Blue => Color.blue,
            LeverColor.Purple => new Color(0.5f, 0f, 0.5f), // Cor roxa
            LeverColor.Yellow => Color.yellow,
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
            Debug.Log("<color=green>GANHOU</color>");
            //Provavelmente ficará assim, irei destruir a barreira temporariamente
            if (_door != null)
            {
                _door.GetComponent<Animator>().SetBool("canOpen", true); // Abre a porta
            }
        }
    }
}
