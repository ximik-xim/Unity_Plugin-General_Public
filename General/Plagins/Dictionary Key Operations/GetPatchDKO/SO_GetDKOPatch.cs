using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "SO Get DKO Patch")]
public class SO_GetDKOPatch : ScriptableObject, IInitScripObj
{
    private FindMBS_DKO_DontDestroy _findMbsDkoDontDestroy;

    [SerializeField] 
    private GetDataSO_MBS_DKO _keyDko;

    private DKOKeyAndTargetAction _Dko;
    [SerializeField] 
    private GetDataSODataDKODataKey _keyGeneralLogic;

    
    /// <summary>
    /// Будет ли сохраняться ссылка на DKODataRund после первого получения(иногда может быть важным нюансом)
    /// </summary>
    [SerializeField] 
    private bool _isSaveDataDKO = true;
    
    private DKODataRund _DkoDataRund;

    /// <summary>
    /// Активен ли этот SO Path
    /// (нужен в случае если нужно что бы обьект был, но мозги не долбил, тупо отключить его)
    /// </summary>
    [SerializeField]
    private bool _isActive = true;
    
    private bool _init = false;
    public bool Init => _init;
    public event Action OnInit;

    private void Awake()
    {
        StartInit();
    }
    
    
    
    public void InitScripObj()
    {
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged -= OnStateChanged;
        EditorApplication.playModeStateChanged += OnStateChanged;
        
        _init = false;
        SceneManager.sceneLoaded -= OnLoadScene;
        if (_findMbsDkoDontDestroy != null) 
        {
            _findMbsDkoDontDestroy.OnInit -= OnInitFind;    
        }

        _findMbsDkoDontDestroy = null;
#endif
        StartInit();
    }
    
#if UNITY_EDITOR
    private void OnStateChanged(PlayModeStateChange state)
    {
        // Если мы вышли из Play Mode и вернулись в обычный режим редактирования
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            _init = false;
            SceneManager.sceneLoaded -= OnLoadScene;
            if (_findMbsDkoDontDestroy != null) 
            {
                _findMbsDkoDontDestroy.OnInit -= OnInitFind;   
            }
            
        }
    }
#endif

    private void StartInit()
    {
        if (_init == false && _isActive == true)
        {
            if (_findMbsDkoDontDestroy == null)
            {
                _findMbsDkoDontDestroy = GameObject.FindObjectOfType<FindMBS_DKO_DontDestroy>();
            }
            
            if (_findMbsDkoDontDestroy == null)
            {
                SceneManager.sceneLoaded -= OnLoadScene;
                SceneManager.sceneLoaded += OnLoadScene;
                return;
            }

            CheckInit();

        }
    }

    private void OnLoadScene(Scene arg0, LoadSceneMode arg1)
    {
        var data = GameObject.FindObjectOfType<FindMBS_DKO_DontDestroy>();
        if (data != null)
        {
            SceneManager.sceneLoaded -= OnLoadScene;
            _findMbsDkoDontDestroy = data;
            CheckInit();
        }
    }
    
    private void CheckInit()
    {
        if (_findMbsDkoDontDestroy.Init == false)
        {
            _findMbsDkoDontDestroy.OnInit += OnInitFind;
            return;
        }

        InitFind();
    }

    private void OnInitFind()
    {
        _findMbsDkoDontDestroy.OnInit -= OnInitFind;
        InitFind();
    }
    
    private void InitFind()
    {
        if (_isSaveDataDKO == true)
        {
            if (_keyDko == null)
            {
                Debug.LogError($"Внимание у DKO с именем {this.name} ключ не был установлен");    
            }

            if (_keyDko.GetData() == null)
            {
                Debug.LogError($"Внимание у DKO с именем {this.name} проблема с ключем типа {_keyDko}, возращаемы им ключ == Null");
            }
            
            if (_findMbsDkoDontDestroy.GetDontDestroyMbsDko.DKOIsAlready(_keyDko.GetData()) == true)
            {
                _Dko = _findMbsDkoDontDestroy.GetDontDestroyMbsDko.GetDKO(_keyDko.GetData());
            }
            else
            {
                Debug.Log($"Указанное DKO Key And Target Action по ключу {_keyDko.GetData()} не было найдено в MBS DKO. SO DKO Path {this.name} не Иниц");
                return;
            }
            
            if (_Dko.ActionIsAlready(_keyGeneralLogic.GetData()) == true)
            {
                _DkoDataRund = _Dko.KeyRun(_keyGeneralLogic.GetData());    
            }
            else
            {
                Debug.Log($"Указанное DKO по ключу {_keyGeneralLogic.GetData()} не было найдено в DKO Key And Target Action. SO DKO Path {this.name} не Иниц");
                return;
            }
            
        }
        
        _init = true;
        OnInit?.Invoke();
    }

    public DKODataRund GetDKO(DKODataRund dkoDataRund = null)
    {
        if (_isSaveDataDKO == true)
        {
            return _DkoDataRund;
        }
        
        _Dko = _findMbsDkoDontDestroy.GetDontDestroyMbsDko.GetDKO(_keyDko.GetData());
        DKODataRund dataDKO = _Dko.KeyRun(_keyGeneralLogic.GetData(), dkoDataRund);
        
        return dataDKO;
    }

    public T GetDKO<T>(DKODataRund dkoDataRund = null) where T : DKODataRund
    {
        DKODataRund dataDKO = GetDKO(dkoDataRund);
        
        T returnData = null;

        if (dataDKO is T == true)
        {
            returnData = (T)dataDKO;
        }

        return returnData;
    }
}
