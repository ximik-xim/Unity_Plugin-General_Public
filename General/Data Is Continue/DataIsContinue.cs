using UnityEngine;

[System.Serializable]
public class DataIsContinue
{
    public DataIsContinue(bool isContinue, string textError = "")
    {
        _isContinue = isContinue;
        _textError = textError;
    }

    private bool _isContinue;
    public bool IsContinue => _isContinue;

    private string _textError;
    public string TextError => _textError;

}
