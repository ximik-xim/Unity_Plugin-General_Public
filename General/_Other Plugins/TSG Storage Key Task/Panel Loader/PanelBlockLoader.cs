using System;
using UnityEngine;

/// <summary>
/// Простая панель для блокировки пока выполняется действие
/// (когда не нужна сложная панель UI Task Loader)
/// </summary>
public class PanelBlockLoader : MonoBehaviour
{
    [SerializeField]
    private GetDKOPatch _patchStorageTask;
    
    [SerializeField] 
    private GetDataSO_TSG_KeyStorageTask _keyStorageTask;
    
    private TSG_StorageKeyTaskDataMono _storageKeyTask;
    private TSG_StorageTaskDefaultData _storageTaskBlock;
    
    [SerializeField]
    private GameObject _panel;
    private void Awake()
    {
        if (_patchStorageTask.Init == false)
        {
            _patchStorageTask.OnInit += OnInitStoragePanel;
            return;
        }

        GetDataDKO();
    }

    private void OnInitStoragePanel()
    {
        _patchStorageTask.OnInit -= OnInitStoragePanel;
        GetDataDKO();
    }

    private void GetDataDKO()
    {
        var DKOData = (DKODataInfoT<TSG_StorageKeyTaskDataMono>)_patchStorageTask.GetDKO();
        _storageKeyTask = DKOData.Data;

        if (_storageKeyTask.IsInit == true)
        {
            Init();
        }
        else
        {
            _storageKeyTask.OnInit += OnInit;
        }
        
    }

    private void OnInit()
    {
        if (_storageKeyTask.IsInit == true) 
        {
            _storageKeyTask.OnInit -= OnInit;
            Init();
        }
    }

    private void Init()
    {
        if (_storageKeyTask.IsKey(_keyStorageTask.GetData()) == false)
        {
            _storageKeyTask.AddTaskData(_keyStorageTask.GetData(), new TSG_StorageTaskDefaultData());
        }
        
        _storageTaskBlock = _storageKeyTask.GetTaskData(_keyStorageTask.GetData());
        _storageTaskBlock.OnUpdateStatus += OnCheckStatusBlockButton;

        OnCheckStatusBlockButton();
    }
    
    private void OnCheckStatusBlockButton()
    {
        if (_storageTaskBlock.IsThereTasks() == true)
        {
            _panel.SetActive(true);
        }
        else
        {
            _panel.SetActive(false);
        }
    }
    
    public void AddKeyTask(TSG_KeyTaskData key, string text)
    {
        _storageTaskBlock.AddTask(key, text);
    }

    public void RemoveKeyTask(TSG_KeyTaskData key)
    {
        _storageTaskBlock.RemoveTask(key);
    }

    public bool IsInsertKeyTask(TSG_KeyTaskData key)
    {
       return _storageTaskBlock.IsKeyTask(key);
    }
}
