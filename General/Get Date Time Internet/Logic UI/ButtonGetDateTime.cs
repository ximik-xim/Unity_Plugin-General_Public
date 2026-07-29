using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Нужна для запуска логики, для получения данных об
/// дате и времени(через сеть)
/// </summary>
public class ButtonGetDateTime : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    
    [SerializeField]
    private GetDateTimeInternet _getDataTimee;

    private void Awake()
    {
        _button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        _getDataTimee.GetCurrentDateTime();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClickButton);
    }
}
