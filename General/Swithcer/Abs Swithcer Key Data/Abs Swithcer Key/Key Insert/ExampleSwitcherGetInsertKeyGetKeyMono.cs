
using System.Collections.Generic;
using UnityEngine;

#if MyStorage
public class ExampleSwitcherGetInsertKeyGetKeyMono : AbsSwitcherGetInsertKeyGetKeyMono<string, Example2KeySwithcer, Example2GetKeyDataMono, Example2GetKeyData>
{
    [SerializeField]
    private List<AbsKeyData<GetDataSOSaveStorageLocation, List<GameObject>>> _listKeyDataGM;

    private void Awake()
    {
        foreach (var VARIABLE in _listKeyDataGM)
        {
            _keyDataGM.Add(VARIABLE.Key.GetData().GetKey(), VARIABLE.Data);
        }
        
        StartAwake();
    }
    
    private void OnEnable()
    {
        if (_keyData != null) 
        {
            CheckEnable();
        }
    }


}
#endif