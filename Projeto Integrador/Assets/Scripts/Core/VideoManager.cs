using System;
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


/*
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class VideoManager : MonoBehaviour
{
    public DropdownField ddpResolution;
    public DropdownField ddpQuality;
    public Toggle tgWindow;

    private List<string> resolutions = new List<string>();
    private List<string> quality = new List<string>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Resolution[] arrResolution = Screen.resolutions;
        foreach(Resolution r in arrResolution){
            resolutions.Add(string.Format("{0} X {1}", r.width, r.height));
        }

        ddpResolution.choices = resolutions;
        ddpResolution.value = resolutions.Last(); //Seleciona a maior resolução disponível

        quality = QualitySettings.names.ToList<String>();
        ddpQuality.choices = quality;
        ddpQuality.value = quality[QualitySettings.GetQualityLevel()]; //Pega a o indice de qualidade atual
    }

    public void SetWindowMode(){
        if(tgWindow.value){ //Retorna se o toogle estiver marcado
            Screen.fullScreen = false;
        }else{
            Screen.fullScreen = true;
        }
    }

    public void SetResolution(){
        string[] res = resolutions[ddpResolution.index].Split('x');
        int w = Convert.ToInt16(res[0].Trim());
        int h = Convert.ToInt16(res[1].Trim());
        Screen.SetResolution(w, h, Screen.fullScreen);
    }

    public void SetQuality(){
        QualitySettings.SetQualityLevel(ddpQuality.index, true);
    }
}
*/