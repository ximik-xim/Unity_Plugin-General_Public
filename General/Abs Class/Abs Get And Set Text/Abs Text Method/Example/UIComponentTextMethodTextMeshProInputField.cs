using System;
using TMPro;
using UnityEngine;

public class UIComponentTextMethodTextMeshProInputField : AbsUIComponentTextMethod
{
    public override bool IsInit => true;
    public override event Action OnInit;

    [SerializeField]
    private TMP_InputField _text;

    private void Awake()
    {
        OnInit?.Invoke();
    }

    public override string GetText()
    {
        return _text.text;
    }

    public override void SetText(string text)
    {
        _text.text = text;
    }

    public override float GetSizeText()
    {
        return _text.textComponent.fontSize;
    }

    public override void SetSizeText(float sizeText)
    {
        _text.textComponent.fontSize = sizeText;
    }
}