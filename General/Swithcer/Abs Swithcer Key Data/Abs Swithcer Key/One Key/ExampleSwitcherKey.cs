using UnityEngine;

#if MyStorage
public class ExampleSwitcherKey : AbsSwitcherKey<YLTypePlatform>
{
    [SerializeField]
    private GetDKOPatch _getDKOPatch;
    
    private YLCurrentPlatformInfoDataGeneralLogic _currentPlatformInfo;
    
    protected override YLTypePlatform GetCurrentKey()
    {
        return _currentPlatformInfo.TypePlatform;
    }

    private void Awake()
    {
        StartInit();
        
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


    private void OnValidate()
    {
        //На случай если SDK не ответит, нужно установить этот тип(как дефолтное значение)
        AddType(YLTypePlatform.None);
        
        AddType(YLTypePlatform.Desktop);
        AddType(YLTypePlatform.Mobile);
        AddType(YLTypePlatform.Tablet);
        AddType(YLTypePlatform.Tv);

        DeletionDuplication();
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