using System;
using UnityEngine;

/// <summary>
/// Нужен для упрощенного получения пути, до Action
/// </summary>
public class GetDKOPatch : MonoBehaviour
{
    [SerializeField] 
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
    
    private bool _init = false;
    public bool Init => _init;
    public event Action OnInit;

    private void Awake()
    {
        if (_findMbsDkoDontDestroy == null)
        {
            _findMbsDkoDontDestroy = FindObjectOfType<FindMBS_DKO_DontDestroy>();
        }
        
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
                Debug.LogError($"Внимание у DKO с именем -> {this.gameObject.name} <- ключ не был установлен");    
            }

            if (_keyDko.GetData() == null)
            {
                Debug.LogError($"Внимание у DKO с именем -> {this.gameObject.name} <- проблема с ключем типа -> {_keyDko} <-, возращаемы им ключ == Null");
            }
            
            
            if (_findMbsDkoDontDestroy.GetDontDestroyMbsDko.DKOIsAlready(_keyDko.GetData()) == true)
            {
                _Dko = _findMbsDkoDontDestroy.GetDontDestroyMbsDko.GetDKO(_keyDko.GetData());
            }
            else
            {
                Debug.LogError($"Указанное DKO Key And Target Action по ключу -> {_keyDko.GetData().GetKey()} <- не было найдено в MBS DKO. SO DKO Path -> {this.name} <- не Иниц");
                return;
            }
            
            if (_Dko.ActionIsAlready(_keyGeneralLogic.GetData()) == true)
            {
                _DkoDataRund = _Dko.KeyRun(_keyGeneralLogic.GetData());    
            }
            else
            {
                Debug.LogError($"Указанное DKO по ключу -> {_keyGeneralLogic.GetData().GetKey()} <- не было найдено в DKO Key And Target Action. DKO Path -> {this.name} <- не Иниц");
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
        
        if (_findMbsDkoDontDestroy.GetDontDestroyMbsDko.DKOIsAlready(_keyDko.GetData()) == true)
        {
            _Dko = _findMbsDkoDontDestroy.GetDontDestroyMbsDko.GetDKO(_keyDko.GetData());
        }
        else
        {
            Debug.LogError($"Указанное DKO Key And Target Action по ключу -> {_keyDko.GetData().GetKey()} <- не было найдено в MBS DKO. SO DKO Path -> {this.name} <- не Иниц");
            return null;
        }
        
        if (_Dko.ActionIsAlready(_keyGeneralLogic.GetData()) == true)
        {
            return _Dko.KeyRun(_keyGeneralLogic.GetData(), dkoDataRund);    
        }
        else
        {
            Debug.LogError($"Указанное DKO по ключу -> {_keyGeneralLogic.GetData().GetKey()} <- не было найдено в DKO Key And Target Action. DKO Path -> {this.name} <- не Иниц");
            return null;
        }
        
        return null;
    }

    public T GetDKO<T>(DKODataRund dkoDataRund = null) where T : DKODataRund
    {
        DKODataRund dataDKO = GetDKO(dkoDataRund);

        T returnData = null;

        if (dataDKO is T == true)
        {
            returnData = (T)dataDKO;
        }
        else
        {
            Debug.LogError($"Не удалось преобразовать тип {dataDKO.GetType()} в тип {typeof(T)}");
        }

        return returnData;
    }
}
