using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ListActionGmSetParent : MonoBehaviour
{
    /// <summary>
    /// Определяем, по какому списку действий пройдемся
    /// или с начало по списку дизактивации обьектов, а потом по списку активации обьектов
    /// или с начало по списку активации обьектов, а потом по списку дизактивации обьектов
    /// </summary>

    [SerializeField] 
    private bool _isActiveAwake;
    
    [SerializeField] 
    private GameObject _parent;

    [SerializeField] 
    private bool _isSetScale = false;

    [SerializeField] 
    private Vector3 _setScale = Vector3.one;
    
    [SerializeField] 
    private bool _isSetLocalPosition = false;
    
    [SerializeField] 
    private Vector3 _setLocalPosition = Vector3.zero;

    /// <summary>
    /// Если false, то будет сохранять локальные коорд. относительно нового род.
    /// Если True, будет сохр. мировые(глобальные) коорд.
    /// </summary>
    [SerializeField]
    private bool _isWorldPositionStays = false;
    
    [SerializeField] 
    private List<GameObject> _gm = new List<GameObject>();
    
    public event Action OnCompletedLogic;
    public bool IsCompletedLogic => _isCompletedLogic;
    private bool _isCompletedLogic = true;
    
    private void Awake()
    {
        if (_isActiveAwake == true)
        {
            StartAction();
        }
    }

    public void StartAction(GameObject parent = null)
    {
        _isCompletedLogic = false;


        foreach (var VARIABLE in _gm)
        {
            if (parent == null)
            {
                VARIABLE.gameObject.transform.SetParent(_parent.transform, _isWorldPositionStays);    
            }
            else
            {
                VARIABLE.gameObject.transform.SetParent(parent.transform, _isWorldPositionStays);
            }
            
            if (_isSetScale == true)
            {
                VARIABLE.gameObject.transform.localScale = _setScale;
            }
            
            if (_isSetLocalPosition == true)
            {
                VARIABLE.gameObject.transform.localPosition = _setLocalPosition;
            }
        }

        _isCompletedLogic = true;
        OnCompletedLogic?.Invoke();
    }

    public IReadOnlyList<GameObject> GetListGm()
    {
        return _gm;
    }
}
