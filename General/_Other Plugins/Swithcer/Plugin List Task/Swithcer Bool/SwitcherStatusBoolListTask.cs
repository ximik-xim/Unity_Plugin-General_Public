using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// При переключении статуса bool будет выполнять один из списков задач
/// </summary>
public class SwitcherStatusBoolListTask : MonoBehaviour
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    [SerializeField]
    private IGetBoolStatusMono _statusDataMono;
    [SerializeField] 
    private bool _checkAwake;
    [SerializeField]
    private bool _checkOnEnable;
    [SerializeField]
    private bool _checkEvent;
    
    /// <summary>
    /// В этом списке все обьекты будут включены, если SDK доступен, если нет, то будут выключены
    /// </summary>
    [SerializeField] 
    private LogicListTaskDKO _taskTrue;

    [SerializeField]
    private LogicListTaskDKO _taskFalse;

    [SerializeField]
    private DKOKeyAndTargetAction _dko;

    [SerializeField]
    private int _priorityEvent = Int32.MaxValue - 1;
    
    private void Awake()
    {
        if (_statusDataMono.IsInit == false)
        {
            _statusDataMono.OnInit += OnInitGetData;  
        }
        
        if (_taskTrue.IsInit == false)
        {
            _taskTrue.OnInit += OnInitTaskTrue;  
        }
        
        if (_taskFalse.IsInit == false)
        {
            _taskFalse.OnInit += OnInitTaskFalse;  
        }
        

        CheckInit();

    }

    private void OnInitTaskTrue()
    {
        if (_taskTrue.IsInit == true)
        {
            _taskTrue.OnInit -= OnInitTaskTrue;
            CheckInit();
        }

    }

    private void OnInitTaskFalse()
    {
        if (_taskFalse.IsInit == true)
        {
            _taskFalse.OnInit -= OnInitTaskFalse;
            CheckInit();
        }
    }

    private void OnInitGetData()
    {
        if (_statusDataMono.IsInit == true) 
        {
            _statusDataMono.OnInit -= OnInitGetData;
            CheckInit();
        }
    }


    private void CheckInit()
    {
        if (_statusDataMono.IsInit == true && _taskTrue.IsInit == true && _taskFalse.IsInit == true) 
        {
            if (_checkEvent == true)
            {
                _statusDataMono.OnUpdateStatus.Subscribe(CheckStatusData, _priorityEvent);
            }

            if (_checkAwake == true)
            {
                CheckStatusData(_statusDataMono.GetStatusBool());
            }
        }
    }
    

    private void OnEnable()
    {
        if (_isInit == true)
        {
            if (_checkOnEnable == true)
            {
                if (_statusDataMono != null)
                {
                    CheckStatusData(_statusDataMono.GetStatusBool());
                }
            }
        }

    }
    
    private void CheckStatusData(bool status)
    {
        if (status == true)
        {
            _taskTrue.StartAction(_dko);
        }
        else
        {
            _taskFalse.StartAction(_dko);
        }
    }

    
    private void OnDestroy()
    {
        if (_statusDataMono != null)
        {
            _statusDataMono.OnUpdateStatus.Unsubscribe(CheckStatusData, _priorityEvent);
        }
    }
}
