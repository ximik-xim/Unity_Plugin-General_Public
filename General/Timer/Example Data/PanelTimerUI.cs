using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Логика UI отображения для таймера
/// </summary>
public class PanelTimerUI : MonoBehaviour
{
    [SerializeField]
    private GetDKOPatch _dkoPatchStorageTimerData;
    private StorageTimerData _storageTimerData;

    [SerializeField]
    private GetDataSO_KeyTimerData _keyTimerData;

    [SerializeField]
    private GameObject _panelTimer;

    [SerializeField]
    private Text _textTimer;

    [SerializeField]
    private bool _reverseTimer;

    private void Awake()
    {
        if (_dkoPatchStorageTimerData.Init == false)
        {
            _dkoPatchStorageTimerData.OnInit += OnInitGetDkoPatch;
        }
        
        CheckInit();
    }
    private void OnInitGetDkoPatch()
    {
        if (_dkoPatchStorageTimerData.Init == true)
        {
            _dkoPatchStorageTimerData.OnInit -= OnInitGetDkoPatch;
            CheckInit();
        }
    }

    private void CheckInit()
    {
        if (_dkoPatchStorageTimerData.Init == true)
        {
            _storageTimerData = _dkoPatchStorageTimerData.GetDKO<DKODataInfoT<StorageTimerData>>().Data;
            Init();
        }
    }

    private void Init()
    {
        if (_storageTimerData.IsContains(_keyTimerData.GetData()) == false)
        {
            _storageTimerData.OnUpdaeStorageData += OnUpdaeStorageData;
        }
        else
        {
            GetTimer();
        }
    }

    private void OnUpdaeStorageData()
    {
        if (_storageTimerData.IsContains(_keyTimerData.GetData()) == true)
        {
            _storageTimerData.OnUpdaeStorageData -= OnUpdaeStorageData;
            GetTimer();
        }
    }

    private void GetTimer()
    {
        var timer = _storageTimerData.GetTimerData(_keyTimerData.GetData());

        if (timer.IsStart == false)
        {
            timer.OnStartTimer += OnStartTimer;
        }
        else
        {
            StartTimer();
        }

        timer.OnUpdateCurrentValue += UpdateTextTimer;
        timer.OnUpdateCompletedTimer += OnUpdateCompletedTimer;
        timer.OnBreakTimer += OnBreakTimer;
    }
    
    private void OnStartTimer(AbsTimerData timer)
    {
        StartTimer();
    }

    private void StartTimer()
    {
        _panelTimer.SetActive(true);
        UpdateTextTimer();
    }

    private void UpdateTextTimer(AbsTimerData timer)
    {
        UpdateTextTimer();
    }
    
    private void UpdateTextTimer()
    {
        var timer = _storageTimerData.GetTimerData(_keyTimerData.GetData());

        if (_reverseTimer == false) 
        {
            _textTimer.text = timer.CurrentValue.ToString();    
        }
        else
        {
            _textTimer.text = (timer.TargetValue - timer.CurrentValue).ToString();
        }
    }
    
    private void OnUpdateCompletedTimer(AbsTimerData timer)
    {
        _panelTimer.SetActive(false);
    }
    
    private void OnBreakTimer(AbsTimerData obj)
    {
        _panelTimer.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_storageTimerData.IsContains(_keyTimerData.GetData()) == true)
        {
            var timer = _storageTimerData.GetTimerData(_keyTimerData.GetData());
            
            timer.OnStartTimer -= OnStartTimer;
            timer.OnUpdateCurrentValue -= UpdateTextTimer;
            timer.OnUpdateCompletedTimer -= OnUpdateCompletedTimer;
            timer.OnBreakTimer -= OnBreakTimer;
        }
    }
}
