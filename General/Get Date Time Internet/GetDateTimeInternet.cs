using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


/// <summary>
/// Получает данные об текущей дате и времени через интернет(webRequest)
/// </summary>
public class GetDateTimeInternet : MonoBehaviour
{
    [SerializeField]
    private string _url = "https://google.com";
    [SerializeField]
    private string _header = "date";
    
    /// <summary>
    /// Список Id callback, которые сейчас в ожидании
    /// (сериализован просто для удобного отслеживания в инспекторе)
    /// </summary>
    [SerializeField]
    private List<int> _idCallback = new List<int>();

    private DateTime _lastDateTime;
    public DateTime LastDateTime => _lastDateTime;
    public event Action OnUpdateDataLastDateTime;
    
    
    public GetServerRequestData<DateTime> GetCurrentDateTime()
    {
        int id = GetUniqueId();
        CallbackRequestDataDateTime wrapperCallbackData = new CallbackRequestDataDateTime(id); 
        
        UnityWebRequest webRequest = UnityWebRequest.Head(_url);
        UnityWebRequestAsyncOperation operation  = webRequest.SendWebRequest();
            
        //Проверяем готов ли ответ
        if (webRequest.isDone == true)
        {
            CompletedRequest();
        }
        else
        {
            operation.completed += OnCompletedRequest;
        }
            
        void OnCompletedRequest(AsyncOperation obj)
        {
            if (webRequest.isDone == true) 
            {
                operation.completed -= OnCompletedRequest;
                CompletedRequest();
            }
        }
            
        //Выводит результат ответа на запрос
        void CompletedRequest()
        {
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                
                string date = webRequest.GetResponseHeader(_header);
                DateTime utc;
                    
                if (DateTime.TryParse(date, out utc) == true)
                {
                    utc.ToUniversalTime();
                }
                
                wrapperCallbackData.Data.StatusServer = StatusCallBackServer.Ok;
                wrapperCallbackData.Data.GetData = utc;
                wrapperCallbackData.Data.IsGetDataCompleted = true;
                wrapperCallbackData.Data.Invoke();

                _lastDateTime = utc;
                OnUpdateDataLastDateTime?.Invoke();
            }
            else
            {
   
                wrapperCallbackData.Data.StatusServer = StatusCallBackServer.Error;
                wrapperCallbackData.Data.GetData = default;
                wrapperCallbackData.Data.IsGetDataCompleted = true;
                wrapperCallbackData.Data.Invoke(); 
            }
        }

        return wrapperCallbackData.DataGet;
    }
    
    private int GetUniqueId()
    {
        int id = 0;
        while (true)
        {
            id = Random.Range(0, Int32.MaxValue - 1);
            if (_idCallback.Contains(id) == false)
            {
                break;
            }
        }

        return id;
    }
}
