using System;
using UnityEngine;

[System.Serializable]
public abstract class AbsTimerData
{
    /// <summary>
    /// Значение которое будет прибавляться
    /// </summary>
    public abstract float AddValue
    {
        get;
    }

    /// <summary>
    /// Интервал между прибавлением значения
    /// </summary>
    public abstract float Delay
    {
        get;
    }

    /// <summary>
    /// Текущее значение
    /// </summary>
    public abstract float CurrentValue
    {
        get;
    }

    /// <summary>
    /// Целевое значение
    /// </summary>
    public abstract float TargetValue
    {
        get;
    }

    /// <summary>
    /// Запущен ли таймер
    /// </summary>
    public abstract bool IsStart
    {
        get;
    }


    public abstract event Action<AbsTimerData> OnUpdateCurrentValue;
    public abstract event Action<AbsTimerData> OnUpdateCompletedTimer;
    public abstract event Action<AbsTimerData> OnStartTimer;
    public abstract event Action<AbsTimerData> OnBreakTimer;

    public abstract void StartTimer(float targetValue, float addValue = 0.1f, float delaySec = 0.1f, float currentValue = 0);

    public abstract void BreakTimer();

    public abstract void OnDestroy();

}
