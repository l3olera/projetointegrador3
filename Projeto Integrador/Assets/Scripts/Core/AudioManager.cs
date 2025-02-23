using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Tooltip("Referência o audioMixer que vai ser alterado")]
    public AudioMixer mainAudioMixer;

    public void SetMasterVol(float vol){
        //Define o volume do grupo master de som
        mainAudioMixer.SetFloat("VolumeMaster", 20f * Mathf.Log10(vol)); //Ele faz cálculo com base em logaritmico para ser proporcional os valores
    }

    public void SetMusicVol(float vol){
        //Define o volume do grupo da música do jogo
        mainAudioMixer.SetFloat("VolumeMusic",  20f * Mathf.Log10(vol));
    }
    
    public void SetSFXVol(float vol){
        //Define o volume do grupo dos efeitos sonoros do jogo
        mainAudioMixer.SetFloat("VolumeSFX",  20f * Mathf.Log10(vol));
    }
}
