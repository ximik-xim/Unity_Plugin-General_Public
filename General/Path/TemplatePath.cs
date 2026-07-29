using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(menuName = "Path/Template Path")]
public class TemplatePath : ScriptableObject
{
    [TextArea(3, 10)]
    [SerializeField]
    private string _template;

    /// <summary>
    /// Список ключей, которые должны быть в шаблоне
    /// </summary>
    [SerializeField]
    private List<GetDataSO_KeyPathData> _keys;


    [SerializeField]
    private string _errorText = "";
    
    /// <summary>
    /// Автоматически исправлять отсутствие указынных ключей
    /// </summary>
    [SerializeField]
    private bool _autoAddKey;
    
    /// <summary>
    /// Автоматически удалять ключей которых нет
    /// </summary>
    [SerializeField]
    private bool _autoRemoveKey;

    [SerializeField]
    private string _startStr = "{:";
    [SerializeField]
    private string _endStr = ":}";
    
    public string GetData(Dictionary<KeyPathData, string> keyAndSetData)
    {
        string result = _template; 
        
        foreach (var VARIABLE in keyAndSetData)
        {
            result = result.Replace(_startStr + VARIABLE.Key.GetKey() + _endStr, VARIABLE.Value);
        }

        return result;
    }

    public string GetData()
    {
        string result = _template; 

        return result;
    }
    
    private void OnValidate()
    {
        _errorText = "";

        if (_autoAddKey == true)
        {
            AddKey();
        }
        else
        {
            foreach (var VARIABLE in _keys)
            {
                if (_template.Contains(_startStr + VARIABLE.GetData().GetKey() + _endStr) == false)
                {
                    _errorText += "Ошибка, не найден ключ " + VARIABLE.GetData().GetKey() + "\n";
                }
            }
        }

        
        string result = _template;

        foreach (var VARIABLE in _keys)
        {
            result = result.Replace(_startStr + VARIABLE.GetData().GetKey() + _endStr, "");
        }
        

        if (result.Length > 0)
        {
            string escapedStart = Regex.Escape(_startStr);
            string pattern = $"(?={escapedStart})";
            
            string[] targetKey = Regex.Split(result, pattern); 
            
            foreach (var VARIABLE in targetKey)
            {
                if (VARIABLE.Length > 0 && VARIABLE.StartsWith(_startStr) == true) 
                {

                    int keyEnd = VARIABLE.IndexOf(_endStr);
                    if (keyEnd == -1)
                    {
                        keyEnd = VARIABLE.Length;
                    }

                    if (_autoRemoveKey == true)
                    {
                        if (_template.Contains(VARIABLE.Substring(0, keyEnd) + _endStr) == true)
                        {
                            _template = _template.Replace(VARIABLE.Substring(0, keyEnd) + _endStr , "");
                        }
                    }
                    else
                    {
                        _errorText += "Ошибка, найден ключ не указанный в списке " + VARIABLE.Substring(_startStr.Length, keyEnd - _startStr.Length) + "\n";
                    }
                }
            }
        }
    }

    private void AddKey()
    {
        foreach (var VARIABLE in _keys)
        {
            if (_template.Contains(_startStr + VARIABLE.GetData().GetKey() + _endStr ) == false)
            {
                _template += _startStr + VARIABLE.GetData().GetKey() + _endStr;
            }     
        }
    }
}
