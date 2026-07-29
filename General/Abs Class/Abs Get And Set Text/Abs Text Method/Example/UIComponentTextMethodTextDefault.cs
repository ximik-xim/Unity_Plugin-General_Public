using System;
using UnityEngine;
using UnityEngine.UI;

public class UIComponentTextMethodTextDefault : AbsUIComponentTextMethod
{
    public override bool IsInit => true;
    public override event Action OnInit;

    [SerializeField]
    private Text _text;

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
        return _text.fontSize;
    }

    public override void SetSizeText(float sizeText)
    {
        _text.fontSize = (int)sizeText;
    }
}
