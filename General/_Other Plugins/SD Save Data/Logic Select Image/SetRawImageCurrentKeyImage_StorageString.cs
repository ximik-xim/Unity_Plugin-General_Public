using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Установит изображение по ключу полученному из SD Storage String
/// </summary>
public class SetRawImageCurrentKeyImage_StorageString : MonoBehaviour
{
    public bool IsInit => _isInit;
    private bool _isInit = false;
    public event Action OnInit;
    
    [SerializeField]
    private SD_AbsStringStorage _storageString;
    
    [SerializeField]
    private GetDataSO_SD_KeyStorageStringVariable _keyDataStorage;

    [SerializeField]
    private GetDKOPatch _patchStorageKeyAndSpriteImage;
    private StorageKeyAndSpriteImage _storageKeyAndSpriteImage;
    
    [SerializeField]
    private RawImage _rawImage;
    
    [SerializeField]
    private GetDKOPatch _patchStorageTask;
    /// <summary>
    /// Автоматически создать ключ хран. блокир
    /// (если будет включен, то ключ ниже будет проигнорирован)
    /// (нужно на случай если не отвественный класс и надо упростить работу)
    /// </summary>
    [SerializeField]
    private bool _autoCreateKeyStorage = true;
    [SerializeField] 
    private GetDataSO_TSG_KeyStorageTask _keyStorageTaskSO;
    private TSG_KeyStorageTask _keyStorageTask;
    private TSG_StorageTaskDefaultData _storageTaskBlock;
    
    /// <summary>
    /// Обновлять ли данные когда хранилеще выгрузило новые данные с сервера
    /// </summary>
    [SerializeField]
    private bool _autoUpdateData = true;
    /// <summary>
    /// Обновлять ли данные когда в хранилеще по указ. ключи изменились данные
    /// </summary>
    [SerializeField]
    private bool _autoUpdateValue = true;
    
    /// <summary>
    /// Обновлять ли данные при включении (OnEnable)
    /// </summary>
    [SerializeField]
    private bool _autoOnEnable = true;
    
    /// <summary>
    /// Если получение данных заблокировано, подпис. ли на ожидание пока блокир. будет снята и потом обнов. данные
    /// </summary>
    [SerializeField]
    private bool _autoCallbackRemoveBlock = true;

    
    private void Awake()
    {
        if (_patchStorageKeyAndSpriteImage.Init == false)
        {
            _patchStorageKeyAndSpriteImage.OnInit += OnInitStoragePanel;
        }
        
        if (_patchStorageTask.Init == false)
        {
            _patchStorageTask.OnInit += OnInitPatchStorageTask;
        }

        
        CheckInit();
    }

    private void OnInitStoragePanel()
    {
        _patchStorageKeyAndSpriteImage.OnInit -= OnInitStoragePanel;
        CheckInit();
    }

    private void OnInitPatchStorageTask()
    {
        if (_patchStorageTask.Init == true)
        {
            _patchStorageTask.OnInit -= OnInitPatchStorageTask;
            CheckInit();
        }
    }

    private void CheckInit()
    {
        if (_patchStorageKeyAndSpriteImage.Init == true && _patchStorageTask.Init == true)
        {
            var DKOData = (DKODataInfoT<StorageKeyAndSpriteImage>)_patchStorageKeyAndSpriteImage.GetDKO();
            _storageKeyAndSpriteImage = DKOData.Data;

            if (_storageKeyAndSpriteImage.IsInit == true)
            {
                Init();
            }
            else
            {
                _storageKeyAndSpriteImage.OnInit -= OnInitStorageKeyAndSpriteImage;
                _storageKeyAndSpriteImage.OnInit += OnInitStorageKeyAndSpriteImage;
            }
            
            var DKODataStorageKeyTask = (DKODataInfoT<TSG_StorageKeyTaskDataMono>)_patchStorageTask.GetDKO();
            var storageKeyTask = DKODataStorageKeyTask.Data;

            if (storageKeyTask.IsInit == false)
            {
                storageKeyTask.OnInit += OnInitStorageKeyTask;
            }
            
            Init();
        }
    }
    
    private void OnInitStorageKeyAndSpriteImage()
    {
        if (_storageKeyAndSpriteImage.IsInit == true)
        {
            _storageKeyAndSpriteImage.OnInit -= OnInitStorageKeyAndSpriteImage;
            Init();
        }
    }
    
    private void OnInitStorageKeyTask()
    {
        var DKOData = (DKODataInfoT<TSG_StorageKeyTaskDataMono>)_patchStorageTask.GetDKO();
        var storageKeyTask = DKOData.Data;
        
        if (storageKeyTask.IsInit == true) 
        {
            storageKeyTask.OnInit -= OnInit;
            Init();
        }
    }

    private void Init()
    {
        var DKODataStorageKeyTask = (DKODataInfoT<TSG_StorageKeyTaskDataMono>)_patchStorageTask.GetDKO();
        var storageKeyTask = DKODataStorageKeyTask.Data;
        
            if (_storageKeyAndSpriteImage.IsInit == true && storageKeyTask.IsInit == true)
        {

            if (_autoCreateKeyStorage == true)
            {
                _keyStorageTask = new TSG_KeyStorageTask($"Хран блокир {this.name} {this.GetHashCode().ToString()}");
            }
            else
            {
                _keyStorageTask = _keyStorageTaskSO.GetData();
            }
        
        
            if (storageKeyTask.IsKey(_keyStorageTask) == false)
            {
                storageKeyTask.AddTaskData(_keyStorageTask, new TSG_StorageTaskDefaultData());
            }

            _storageTaskBlock = storageKeyTask.GetTaskData(_keyStorageTask);

            
            _storageString.OnUpdateData += OnUpdateDataSetData;
            _storageString.OnUpdateValue += OnUpdateValueSetData;

            _isInit = true;
            OnInit?.Invoke();
            
            CheckSetImage();
        }
    }

    private void OnUpdateDataSetData()
    {
        if (_autoUpdateData == true) 
        {
            CheckSetImage();
        }
    }

    private void OnUpdateValueSetData(SD_KeyStorageStringVariable key)
    {
        if (_keyDataStorage.GetData().GetKey() == key.GetKey())
        {
            if (_autoUpdateValue == true)
            {
                CheckSetImage();
            }
        }
    }
    
    private void OnEnable()
    {
        if (_autoOnEnable == true)
        {
            CheckSetImage();
        }
    }
    
    private void CheckSetImage()
    {
        if (_storageTaskBlock != null)
        {
            if (_storageTaskBlock.IsThereTasks() == false)
            {
                SetImage();
            }
            else
            {
                if (_autoCallbackRemoveBlock == true)
                {
                    _storageTaskBlock.OnUpdateStatus -= OnSetImage;
                    _storageTaskBlock.OnUpdateStatus += OnSetImage;
                }
                
            }
        }
    }
    
    private void OnSetImage()
    {
        if (_storageTaskBlock.IsThereTasks() == false)
        {
            _storageTaskBlock.OnUpdateStatus -= OnSetImage;   
            SetImage();
        }
    }
    
    private void SetImage()
    {
        if (_storageKeyAndSpriteImage.IsInit == true)
        {
            if (_storageString.IsThereData(_keyDataStorage.GetData()) == true)
            {
                CustomMethodImage.SetSpriteRawImage(_rawImage, _storageKeyAndSpriteImage.GetImage(new KeyImage(_storageString.GetData(_keyDataStorage.GetData()))));    
            }
        }
    }

    
    public void AddTaskBlock(TSG_KeyTaskData key, string textBlockTask)
    {
        _storageTaskBlock.AddTask(key, textBlockTask);
    }

    public void RemoveTaskBlock(TSG_KeyTaskData key)
    {
        _storageTaskBlock.RemoveTask(key);
    }

    private void OnDestroy()
    {
        _storageString.OnUpdateData -= OnUpdateDataSetData;
        _storageString.OnUpdateValue -= OnUpdateValueSetData;
    }
}
