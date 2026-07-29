using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Хранилище с таймерами
/// </summary>
public class StorageTimerData : MonoBehaviour
{
    public event Action OnUpdaeStorageData;
    
    private Dictionary<string, AbsTimerData> _storageTimer = new Dictionary<string, AbsTimerData>();

    public void AddTimer(KeyTimerData key, AbsTimerData timerData)
    {
        if (_storageTimer.ContainsKey(key.GetKey()) == false)
        {
            _storageTimer.Add(key.GetKey(), timerData);
        }
        
        OnUpdaeStorageData?.Invoke();
    }

    public AbsTimerData GetTimerData(KeyTimerData key)
    {
        if (_storageTimer.ContainsKey(key.GetKey()) == true)
        {
            return _storageTimer[key.GetKey()];
        }

        return null;
    }

    public bool IsContains(KeyTimerData key)
    {
        return _storageTimer.ContainsKey(key.GetKey());
    }

    public void RemoveTime(KeyTimerData key, bool isDestroyTimer = true)
    {
        if (isDestroyTimer == true)
        {
            _storageTimer[key.GetKey()].OnDestroy();
        }

        _storageTimer.Remove(key.GetKey());
        
        OnUpdaeStorageData?.Invoke();
    }

    private void OnDestroy()
    {
        foreach (var Key in _storageTimer.Keys)
        {
            _storageTimer[Key].OnDestroy();
        }
    }
}
