using System;
using UnityEngine;

/// <summary>
/// Пример использования таймеров
/// </summary>
public class ExampleUseTimer : MonoBehaviour
{
   [SerializeField]
   private GetDataSO_KeyTimerData _keyTimerDataCorutine;
   
   [SerializeField]
   private GetDataSO_KeyTimerData _keyTimerDataAsync;
   
   [SerializeField]
   private GetDataSO_KeyTimerData _keyTimerDataAsyncUniTask;

   [SerializeField]
   private StorageTimerData _storageTimerData;
   
   private void Start()
   {
      _storageTimerData.AddTimer(_keyTimerDataCorutine.GetData(), new TimerCoroutine(_storageTimerData));
      
      _storageTimerData.AddTimer(_keyTimerDataAsync.GetData(), new TimerAsync());
      
      _storageTimerData.AddTimer(_keyTimerDataAsyncUniTask.GetData(), new TimerAsyncUniTask());


      StartTimer();
   }

   private void StartTimer()
   {
      AbsTimerData timer;
      timer = _storageTimerData.GetTimerData(_keyTimerDataCorutine.GetData());

      timer.OnStartTimer += OnStartTimer;
      timer.OnUpdateCurrentValue += OnUpdateCurrentValue;
      timer.OnUpdateCompletedTimer += OnUpdateCompletedTimer;
      timer.OnBreakTimer += OnBreakTimer;

      timer.StartTimer(5, 1, 0.5f);
   }

   private void OnStartTimer(AbsTimerData timerData)
   {
      Debug.Log("Таймер запущен");
   }

   private void OnUpdateCurrentValue(AbsTimerData timerData)
   {
      Debug.Log("Тек знач = " + timerData.CurrentValue);
   }

   private void OnUpdateCompletedTimer(AbsTimerData timerData)
   {
      Debug.Log("Таймер закончил работу");
      
      timerData.StartTimer(5, 1, 0.5f);
      
      timerData.BreakTimer();
   }

   private void OnBreakTimer(AbsTimerData timerData)
   {
      Debug.Log("Таймер был прерван");
      
      timerData.OnStartTimer -= OnStartTimer;
      timerData.OnUpdateCurrentValue -= OnUpdateCurrentValue;
      timerData.OnUpdateCompletedTimer -= OnUpdateCompletedTimer;
      timerData.OnBreakTimer -= OnBreakTimer;
      
      AbsTimerData timer;
      timer = _storageTimerData.GetTimerData(_keyTimerDataAsync.GetData());

      timer.OnStartTimer += OnStartTimer2;
      timer.OnUpdateCurrentValue += OnUpdateCurrentValue2;
      timer.OnUpdateCompletedTimer += OnUpdateCompletedTimer2;
      timer.OnBreakTimer += OnBreakTimer2;
      
      timer.StartTimer(5, 1, 0.5f);
   }
   
   private void OnStartTimer2(AbsTimerData timerData)
   {
      Debug.Log("Таймер запущен");
   }

   private void OnUpdateCurrentValue2(AbsTimerData timerData)
   {
      Debug.Log("Тек знач = " + timerData.CurrentValue);
   }

   private void OnUpdateCompletedTimer2(AbsTimerData timerData)
   {
      Debug.Log("Таймер закончил работу");
      
      timerData.StartTimer(5, 1, 0.5f);
      
      timerData.BreakTimer();
   }

   private void OnBreakTimer2(AbsTimerData timerData)
   {
      Debug.Log("Таймер был прерван");
      
      timerData.OnStartTimer -= OnStartTimer2;
      timerData.OnUpdateCurrentValue -= OnUpdateCurrentValue2;
      timerData.OnUpdateCompletedTimer -= OnUpdateCompletedTimer2;
      timerData.OnBreakTimer -= OnBreakTimer2;
      
      AbsTimerData timer;
      timer = _storageTimerData.GetTimerData(_keyTimerDataAsyncUniTask.GetData());

      timer.OnStartTimer += OnStartTimer3;
      timer.OnUpdateCurrentValue += OnUpdateCurrentValue3;
      timer.OnUpdateCompletedTimer += OnUpdateCompletedTimer3;
      timer.OnBreakTimer += OnBreakTimer3;
      
      timer.StartTimer(5, 1, 0.5f);
   }
   
   private void OnStartTimer3(AbsTimerData timerData)
   {
      Debug.Log("Таймер запущен");
   }

   private void OnUpdateCurrentValue3(AbsTimerData timerData)
   {
      Debug.Log("Тек знач = " + timerData.CurrentValue);
   }

   private void OnUpdateCompletedTimer3(AbsTimerData timerData)
   {
      Debug.Log("Таймер закончил работу");
      
      timerData.StartTimer(5, 1, 0.5f);
      
      timerData.BreakTimer();
   }

   private void OnBreakTimer3(AbsTimerData timerData)
   {
      Debug.Log("Таймер был прерван");
      
      timerData.OnStartTimer -= OnStartTimer3;
      timerData.OnUpdateCurrentValue -= OnUpdateCurrentValue3;
      timerData.OnUpdateCompletedTimer -= OnUpdateCompletedTimer3;
      timerData.OnBreakTimer -= OnBreakTimer3;
      

      Debug.Log("Все таймеры отработали");
   }
}
