using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TimerAsync : AbsTimerData
{
    private CancellationTokenSource _token;
    
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
        StartTimerAsync(targetValue, addValue, delaySec, currentValue);
    }

    private async Task StartTimerAsync(float targetValue, float addValue = 0.1f, float delaySec = 0.1f, float currentValue = 0)
    {
        if (_isStart == false)
        {
            _isStart = true;
            // Очищаем старый токен на всякий случай
            _token?.Dispose();
            _token = new CancellationTokenSource();
            
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
            
            try
            {
                await RunTimerAsync();
                Completed();
            }
            catch (OperationCanceledException)
            {
                // Это нормальное поведение при отмене, просто уведомляем
                OnBreakTimer?.Invoke(this);
            }
            catch (Exception e)
            {
                Debug.LogError($"Ошибка в таймере: {e.Message}");
            }
            
          
        }
    }
    
    private async Task RunTimerAsync()
    {
        int delayMilliseconds = Mathf.RoundToInt(_delay * 1000);

        while (_currentValue < _targetValue)
        {
            if (_isStart == true)
            {
                // Проверяем, не просили ли нас остановиться
                _token.Token.ThrowIfCancellationRequested();

                _currentValue += _addValue;
                if (_currentValue > _targetValue)
                {
                    _currentValue = _targetValue;
                }

                OnUpdateCurrentValue?.Invoke(this);

                // Асинхронное ожидание (не блокирует основной поток)
                await Task.Delay(delayMilliseconds, _token.Token);
            }
            else
            {
                return;
            }
        }

        Completed();
    }

    public override void BreakTimer()
    {
        if (_token != null)
        {
            _token.Cancel();
            _token.Dispose();
            _token = null;
        }
        
        _isStart = false;
        
        OnBreakTimer?.Invoke(this);
    }

    private void Completed()
    {
        _isStart = false;
        OnUpdateCompletedTimer?.Invoke(this);
    }

    public override void OnDestroy()
    {
        BreakTimer();
    }
}
