using System;
using TMPro;
using UnityEngine;

public class GetAndSetTextTextMeshPro : AbsGetAndSetText
{
    public override bool IsInit => true;
    public override event Action OnInit;

    [SerializeField]
    private TextMeshProUGUI _text;

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
}
