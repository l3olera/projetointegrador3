using UnityEngine;
using UnityEngine.Localization;


[CreateAssetMenu(fileName = "NewAnimal", menuName = "ScriptableObjects/Animal", order = 1)]
public class Animal : ScriptableObject
{
    public LocalizedString animalName; // Nome do animal
    public AudioClip[] animalSounds; // Sons do animal

    public void PlaySound(AudioSource audioSource)
    {
        if (audioSource != null && animalSounds != null && animalSounds.Length > 0)
        {
            audioSource.clip = animalSounds[Random.Range(0, animalSounds.Length)]; // Define o clipe de áudio
            audioSource.Play(); // Toca o som
        }
    }
}
