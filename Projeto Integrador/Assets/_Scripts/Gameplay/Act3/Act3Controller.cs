using TMPro;
using UnityEngine;

public class Act3Controller : MonoBehaviour
{
    [Header("Componentes Referenciados")]
    [SerializeField] private TextMeshPro _textReset;
    private LocalizationManager _localization;

    [Header("Atributo para o texto de resetar")]
    [SerializeField] private string _translateKey;

    void OnEnable()
    {
        LocalizationManager.OnFinishTranslations += SetTextReset;
    }

    void OnDisable()
    {
        LocalizationManager.OnFinishTranslations -= SetTextReset;
    }

    void Start()
    {
        _localization = LocalizationManager.Instance;
    }

    void SetTextReset()
    {
        _textReset.text = _localization.GetTranslation(_translateKey);
    }
}