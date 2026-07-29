
using UnityEngine;

[System.Serializable]
public class TaskInfo 
{
    [SerializeField]
    private string _text;

    public TaskInfo(string text)
    {
        _text = text;
    }

    public string GetTextTask()
    {
        return _text;
    }
}
