using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    [System.Serializable]
    public class TranslationEntry
    {
        public string key; // Um nome amigável para identificação
        public LocalizedString localizedString; // Referência ao LocalizedString no inspector
    }

    public static event Action OnFinishTranslations;

    [Header("Lista de Traduções")]
    public List<TranslationEntry> translations;

    private Dictionary<string, string> _cachedTranslations = new();

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            await LoadAllTranslations();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async Task LoadAllTranslations()
    {
        foreach (var entry in translations)
        {
            var handle = entry.localizedString.GetLocalizedStringAsync();
            await handle.Task;
            var localizedString = handle.Result;
            _cachedTranslations[entry.key] = localizedString;
        }
        OnFinishTranslations?.Invoke();
    }

    public string GetTranslation(string key)
    {
        if (_cachedTranslations.TryGetValue(key, out var value))
        {
            return value;
        }
        else
        {
            return $"[{key}]";
        }
    }
}
