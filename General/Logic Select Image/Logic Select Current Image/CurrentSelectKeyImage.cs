using System;
using UnityEngine;

public class CurrentSelectKeyImage : MonoBehaviour
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    [SerializeField]
    private GetDataSO_KeyImage _defaultKey;

    public KeyImage KeyImage => _keyImage;
    private KeyImage _keyImage;
    public event Action OnUpdateKeyImage;
    
    private void Awake()
    {
        _keyImage = _defaultKey.GetData();
        
        _isInit = true;
        OnInit?.Invoke();
        
        OnUpdateKeyImage?.Invoke();
    }

    public void SetKey(KeyImage key)
    {
        _keyImage = key;
        OnUpdateKeyImage?.Invoke();
    }
}
