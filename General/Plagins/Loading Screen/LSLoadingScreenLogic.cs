using System.Collections.Generic;
using UnityEngine;

public class LSLoadingScreenLogic : MonoBehaviour
{

    [SerializeField] 
    private GameObject _panelLoadingScreen;


    private List<LSLoadingScreenLogicDataTask> _listTask = new List<LSLoadingScreenLogicDataTask>();
    [SerializeField]
    private bool _isOpen = false;
    public bool IsOpen => _isOpen;
    public void AddTask(LSLoadingScreenLogicDataTask task)
    {
        if (_listTask.Count == 0)
        {
            Open();
        }
        
        if (_listTask.Contains(task) == true)
        {
            _listTask.Remove(task);
        }

        _listTask.Add(task);
    }


    public void RemoveTask(LSLoadingScreenLogicDataTask task)
    {
        _listTask.Remove(task);

        CheckComlited();
    }

    private void CheckComlited()
    {
        if (_listTask.Count == 0)
        {
            Close();
        }
    }

    public void Open()
    {
        _isOpen = true;
        _panelLoadingScreen.SetActive(true);
    }

    public void Close()
    {
        _isOpen = false;
        _panelLoadingScreen.SetActive(false);
    }
}

public class LSLoadingScreenLogicDataTask
{
    
}