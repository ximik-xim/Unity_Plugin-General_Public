using System;
using System.Collections.Generic;
using AdvancedInputFieldPlugin;
using UnityEngine;

/// <summary>
/// Нужен что бы можно было по значению ключа А, вернуть ключ Б
/// (заменить значения)
/// </summary>
public class AdvancedInputFieldFilterReplacementKey : MonoBehaviour
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    /// <summary>
    /// Список ключей для замены
    /// </summary>
    [SerializeField]
    private List<AbsKeyData<GetDataSO_KeyDropdown, string>> _listExceptionSetText = new List<AbsKeyData<GetDataSO_KeyDropdown, string>>();
    private Dictionary<string, string> _exceptionSetText = new Dictionary<string, string>();
    
    [SerializeField]
    private AdvancedInputField _advancedInputField;
    
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        foreach (var keyException in _listExceptionSetText)
        {
            _exceptionSetText.Add(keyException.Key.GetData().GetKey(), keyException.Data);
        }
        
        _isInit = true;
        OnInit?.Invoke();
    }

    public KeyDropdown GetKey(KeyDropdown key)
    {
        if (_exceptionSetText.ContainsKey(key.GetKey()) == true)
        {
            return new KeyDropdown(_exceptionSetText[key.GetKey()]);
        }

        return null;
    }

    public bool IsContainsKey(KeyDropdown key)
    {
        return _exceptionSetText.ContainsKey(key.GetKey());
    }

    
}
