
using System.Collections.Generic;
using UnityEngine;

#if MyStorage
public class ExampleSwitcherGetInsertKey : AbsSwitcherKeyData<string>
{
    //Просто как первый попавшийся пример, ток в норм.  реализации тут будет DKO котор. вернет именно нужный ключ
    [SerializeField]
    private List<AbsKeyData<GetDataSOSaveStorageLocation, List<GameObject>>> _listKeyDataGM;

    [SerializeField]
    private GetDKOPatch _getDKOPatch;
    
    private YLCurrentPlatformInfoDataGeneralLogic _currentPlatformInfo;
    
    protected override string GetCurrentKey()
    {
        return ""; //_currentPlatformInfo.TypePlatform;
    }
    
    private void Awake()
    {
        foreach (var VARIABLE in _listKeyDataGM)
        {
            _keyDataGM.Add(VARIABLE.Key.GetData().GetKey(), VARIABLE.Data);
        }
        
        if (_getDKOPatch.Init == false)
        {
            _getDKOPatch.OnInit += OnInitGetData;  
            return;
        }

        InitGetData();
        
    }

    private void OnInitGetData()
    {
        _getDKOPatch.OnInit -= OnInitGetData;
        InitGetData();
    }
    
    private void InitGetData()
    {
        _currentPlatformInfo = (YLCurrentPlatformInfoDataGeneralLogic) _getDKOPatch.GetDKO();
        
        _currentPlatformInfo.OnUpdateCurrentPlatform += CheckEvent;
        
        if (_currentPlatformInfo.Init == false)
        {
            _currentPlatformInfo.OnInit += OnInit;
            return;
        }

        Init();
        
    }
    
    private void OnInit()
    {
        _currentPlatformInfo.OnInit -= OnInit;
        Init();
    }
    
    private void Init()
    {
        
        CheckAwake();
    }

    private void OnEnable()
    {
        if (_currentPlatformInfo != null && _currentPlatformInfo.Init == true) 
        {
            CheckEnable();
        }
    }
    
    private void OnDestroy()
    {
        if (_currentPlatformInfo != null)
        {
            _currentPlatformInfo.OnUpdateCurrentPlatform -= CheckEvent;    
        }
    }
}
#endif