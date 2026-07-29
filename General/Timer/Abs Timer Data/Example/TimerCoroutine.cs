using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCoroutine : AbsTimerData
{
    public TimerCoroutine(MonoBehaviour gameObjectCorutine)
    {
        _gmCorutine = gameObjectCorutine;
    }

    private MonoBehaviour _gmCorutine;
    private IEnumerator _targetTimer;

    /// <summary>
    /// Значение которое будет прибавляться
    /// </summary>
    public override float AddValue => _addValue;
    private float _addValue;

    /// <summary>
    /// Интервал между прибавлением значения
    /// </summary>
    public override float Delay => _delay;
    private float _delay;

    /// <summary>
    /// Текущее значение
    /// </summary>
    public override float CurrentValue => _currentValue;
    private float _currentValue;
    
    /// <summary>
    /// Целевое значение
    /// </summary>
    public override float TargetValue => _targetValue;
    private float _targetValue;

    /// <summary>
    /// Запущен ли таймер
    /// </summary>
    public override bool IsStart => _isStart;
    private bool _isStart = false;
    

    public override event Action<AbsTimerData> OnUpdateCurrentValue;
    public override event Action<AbsTimerData> OnUpdateCompletedTimer;
    public override event Action<AbsTimerData> OnStartTimer;
    public override event Action<AbsTimerData> OnBreakTimer;
    
    public override void StartTimer(float targetValue, float addValue = 0.1f, float delaySec = 0.1f, float currentValue = 0)
    {
        if (_isStart == false)
        {
            _isStart = true;
        
            _targetTimer = StepTimer();

            _targetValue = targetValue;
            _addValue = addValue;
            if (_addValue <= 0) 
            {
                Debug.Log("Ошибка, добавляемое значение у таймера установлено == 0, значение было установлено на 0.1f");
                _addValue = 0.1f;
            }
            
            _delay = delaySec;
            _currentValue = currentValue;
        
            OnStartTimer?.Invoke(this);
            
            // Запускаем таймер
            _gmCorutine.StartCoroutine(_targetTimer);
        }
        
    }
    
    private void Completed()
    {
        _isStart = false;
        OnUpdateCompletedTimer?.Invoke(this);
    }

    public override void BreakTimer()
    {
        if (_targetTimer != null)
        {
           _gmCorutine.StopCoroutine(_targetTimer);
        }
        
        _isStart = false;
        
        OnBreakTimer?.Invoke(this);
    }

    IEnumerator StepTimer()
    {
        while (_currentValue < _targetValue)
        {
            if (_isStart == true)
            {
                _currentValue += _addValue;
                OnUpdateCurrentValue?.Invoke(this);
                
                yield return new WaitForSeconds(_delay);    
            }
            else
            {
                yield return null;
            }
        }

        Completed();
    }

    public override void OnDestroy()
    {
        BreakTimer();
    }
}
