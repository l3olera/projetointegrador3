using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    public Dropdown ddpResolution;
    public Dropdown ddpQuality;
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

        ddpResolution.AddOptions(resolutions);
        ddpResolution.value = (resolutions.Count - 1);

        quality = QualitySettings.names.ToList<String>();
        ddpQuality.AddOptions(quality);
        ddpQuality.value = QualitySettings.GetQualityLevel(); //Pega a o indice de qualidade atual
    }

    public void SetWindowMode(){
        if(tgWindow.isOn){ //Retorna se o toogle estiver marcado
            Screen.fullScreen = false;
        }else{
            Screen.fullScreen = true;
        }
    }

    public void SetResolution(){
        string[] res = resolutions[ddpResolution.value].Split('x');
        int w = Convert.ToInt16(res[0].Trim());
        int h = Convert.ToInt16(res[1].Trim());
        Screen.SetResolution(w, h, Screen.fullScreen);
    }

    public void SetQuality(){
        QualitySettings.SetQualityLevel(ddpQuality.value, true);
    }
}
