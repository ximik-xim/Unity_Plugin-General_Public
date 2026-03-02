using System;
using AdvancedInputFieldPlugin;
using UnityEngine;

/// <summary>
/// Установка текста через плагин(кастомный InputField)
/// </summary>
public class GetAndSetTextAdvancedInputField : AbsGetAndSetText
{
    public override bool IsInit => true;
    public override event Action OnInit;

    [SerializeField]
    private AdvancedInputField _advancedInputField;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override string GetText()
    {
        return _advancedInputField.Text;
    }

    public override void SetText(string text)
    {
        _advancedInputField.SetText(text, true);
    }
}
