using System;
using AdvancedInputFieldPlugin;
using UnityEngine;

public class TriggerEventInputFieldCustomAdvancedInputField : AbsTriggerEventInputFieldCustom
{
    [SerializeField]
    private AdvancedInputField _advancedInputField;

    public override event Action OnOpenInputField;
    public override event Action OnCloseInputField;
    public override event Action<string> OnUpdateText;

    private void Awake()
    {
        _advancedInputField.OnBeginEdit.AddListener(OnBeginEdit);
        _advancedInputField.OnEndEdit.AddListener(OnEndEdit);
        _advancedInputField.OnValueChanged.AddListener(OnTextChanged);
    }

    
    private void OnBeginEdit(BeginEditReason arg0)
    {
        OnOpenInputField?.Invoke();
    }
    
    private void OnEndEdit(string arg0, EndEditReason arg1)
    {
        OnCloseInputField?.Invoke();
    }

    private void OnTextChanged(string text)
    {
        OnUpdateText?.Invoke(text);
    }
    

    private void OnDestroy()
    {
        _advancedInputField.OnBeginEdit.RemoveListener(OnBeginEdit);
        _advancedInputField.OnEndEdit.RemoveListener(OnEndEdit);
        _advancedInputField.OnValueChanged.RemoveListener(OnTextChanged);
    }
}
