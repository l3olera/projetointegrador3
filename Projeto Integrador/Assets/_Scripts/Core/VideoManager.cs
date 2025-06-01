using System.Linq;
using UnityEngine;
using TMPro; // Importa TextMeshPro
using UnityEngine.UI;

public class VideoSettingsManager : MonoBehaviour
{
    public TMP_Dropdown dropdownResolution; // Agora usa TMP_Dropdown
    public Toggle toggleWindow;

    void Start()
    {
        // Adiciona as resoluções disponíveis
        dropdownResolution.ClearOptions();
        dropdownResolution.AddOptions(Screen.resolutions
            .Select(res => $"{res.width} x {res.height}")
            .Distinct()
            .ToList());

        // Define a resolução atual como padrão
        dropdownResolution.value = dropdownResolution.options
            .FindIndex(option => option.text == $"{Screen.currentResolution.width} x {Screen.currentResolution.height}");

        // Define o estado inicial do toggle (true se estiver em modo janela)
        toggleWindow.isOn = !Screen.fullScreen;

        // Adiciona eventos para detectar mudanças no UI
        dropdownResolution.onValueChanged.AddListener(SetResolution);
        toggleWindow.onValueChanged.AddListener(SetWindowMode);
    }

    void SetResolution(int index)
    {
        string[] res = dropdownResolution.options[index].text.Split('x');
        int width = int.Parse(res[0].Trim());
        int height = int.Parse(res[1].Trim());
        Screen.SetResolution(width, height, Screen.fullScreen);
    }

    void SetWindowMode(bool isWindowed)
    {
        Screen.fullScreen = !isWindowed;
    }
}