using System;
using UnityEngine;

public class LogicSetCurrentKeyImage : MonoBehaviour
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    private CurrentSelectKeyImage _currentSelectKeyImage;
    public KeyImage KeyImage => _keyImage;
    private KeyImage _keyImage;

    public void Init(CurrentSelectKeyImage currentSelectKeyImage, KeyImage key)
    {
        _currentSelectKeyImage = currentSelectKeyImage;
        _keyImage = key;
        
              
        _isInit = true;
        OnInit?.Invoke();
    }

    public void SetKey()
    {
        if (_isInit == true)
        {
            _currentSelectKeyImage.SetKey(_keyImage);    
        }
    }

}
